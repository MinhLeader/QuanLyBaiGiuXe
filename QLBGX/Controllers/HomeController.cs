using Microsoft.AspNetCore.Mvc;
using QLBGX.Models;
using QLBGX.ViewModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace QLBGX.Controllers
{
    public class HomeController : Controller
    {
        private readonly QuanLyBaiGiuXeContext _context;

        public HomeController(QuanLyBaiGiuXeContext context)
        {
            _context = context;
        }

        // Hiển thị trang chủ
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Booking()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        // Hiển thị trang Đăng nhập
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Xử lý thông tin Đăng nhập
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _context.TaiKhoans
                    .Include(t => t.MaLoaiTaiKhoanNavigation)
                    .FirstOrDefaultAsync(m => m.TenDangNhap == model.TenDangNhap && m.MatKhau == model.MatKhau);

                if (user != null)
                {
                    // Đăng nhập thành công
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.TenDangNhap),
                        new Claim(ClaimTypes.NameIdentifier, user.MaTaiKhoan.ToString()),
                        new Claim(ClaimTypes.Role, user.MaLoaiTaiKhoanNavigation.TenLoaiTaiKhoan)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true, // Nếu muốn giữ đăng nhập sau khi đóng trình duyệt
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    var loaiTaiKhoan = user.MaLoaiTaiKhoanNavigation.TenLoaiTaiKhoan;
                    if (loaiTaiKhoan == "User")
                    {
                        return RedirectToAction("Booking", "Home"); // Chuyển hướng đến trang chủ
                    }
                    else if (loaiTaiKhoan == "NhanVien" || loaiTaiKhoan == "Admin")
                    {
                        return RedirectToAction("Dashboard", "Admin"); // Chuyển hướng đến trang quản trị
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không chính xác.");
                }
            }
            return View(model);
        }

        // Hiển thị trang Đăng ký
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Xử lý thông tin Đăng ký
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userExists = await _context.TaiKhoans
                    .AnyAsync(u => u.TenDangNhap == model.TenDangNhap);

                if (!userExists)
                {
                    var newUser = new TaiKhoan
                    {
                        TenDangNhap = model.TenDangNhap,
                        MatKhau = model.MatKhau, // Mật khẩu cần được mã hóa trước khi lưu
                        MaLoaiTaiKhoan = 2 // Giả sử 2 là mã cho loại tài khoản 'User'
                                           // MaKH hoặc MaNV có thể được thiết lập dựa trên loại người dùng đăng ký
                    };

                    _context.Add(newUser);
                    await _context.SaveChangesAsync();

                    // Đăng ký thành công, chuyển hướng người dùng tới trang đăng nhập
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập đã tồn tại.");
                }
            }
            return View(model);
        }

       
       
    }
}
