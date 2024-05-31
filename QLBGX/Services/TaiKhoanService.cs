using Microsoft.EntityFrameworkCore;
using QLBGX.Models;
using System.Security.Claims;
using System.Threading.Tasks;

public class TaiKhoanService
{
    private readonly QuanLyBaiGiuXeContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TaiKhoanService(QuanLyBaiGiuXeContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<KhachHang> GetLoggedInUserAsync()
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier); // Lấy claim
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            var taiKhoan = await _context.TaiKhoans.Include(t => t.MaKhNavigation)
                .FirstOrDefaultAsync(t => t.MaTaiKhoan == userId); // So sánh với MaTaiKhoan
            return taiKhoan?.MaKhNavigation;
        }
        return null;
    }
}
