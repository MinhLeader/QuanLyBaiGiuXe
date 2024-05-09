// LoginController.cs
using QLBGX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBGX.ViewModel;

namespace QLBGX.Controllers
{
    public class LoginController : Controller
    {
        private readonly QuanLyBaiGiuXeContext _context;

        public LoginController(QuanLyBaiGiuXeContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.TaiKhoans
                    .Include(u => u.MaLoaiTaiKhoanNavigation)
                    .Include(u => u.MaNvNavigation)
                    .FirstOrDefault(u => u.TenDangNhap == model.TenDangNhap && u.MatKhau == model.MatKhau);

                if (user != null)
                {
                    // Đăng nhập thành công, xác định vai trò và thực hiện phân quyền
                    if (user.MaLoaiTaiKhoanNavigation.TenLoaiTaiKhoan == "Quan Lý")
                    {
                        // Nếu là quản lý, chuyển hướng đến trang quản lý
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Nếu là nhân viên, chuyển hướng đến trang nhân viên
                        return RedirectToAction("Privacy", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không chính xác.");
                }
            }
            return View(model);
        }
    }
}
