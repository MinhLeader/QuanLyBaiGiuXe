using Microsoft.AspNetCore.Mvc;
using QLBGX.Models;
using Microsoft.EntityFrameworkCore; // Thêm thư viện này để sử dụng Include
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
// Thêm các thư viện cần thiết khác

namespace QLBGX.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly QuanLyBaiGiuXeContext _context; // Thay thế YourDbContext bằng tên DbContext của bạn

        public EmployeeController(QuanLyBaiGiuXeContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewData["MaChucVu"] = new SelectList(_context.ChucVus, "MaChucVu", "TenChucVu");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNv,TenNv,SoDienThoai,DiaChi,Email,MaChucVu")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Employee));
            }
            ViewData["MaChucVu"] = new SelectList(_context.ChucVus, "MaChucVu", "TenChucVu", nhanVien.MaChucVu);
            return View(nhanVien);
        }
        // GET: Employee/AddEmployee
        //public IActionResult AddEmployee()
        //{
        //    // Tạo View Model nếu cần
        //    //var chucVuList = _context.ChucVus.ToList();
        //    //if (chucVuList == null || !chucVuList.Any())
        //    //{
        //    //    chucVuList = new List<ChucVu>();
        //    //}
        //    //ViewBag.ChucVu = _context.ChucVus.ToList(); // Lấy danh sách chức vụ từ database
        //    //return View();
        //    // Tải danh sách ChucVu để người dùng có thể chọn
        //    ViewData["MaChucVu"] = new SelectList(_context.ChucVus, "MaChucVu", "TenChucVu");
        //    return View();
        //}

        // POST: Employee/AddEmployee
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddEmployee([Bind("MaNv,TenNv,SoDienThoai,DiaChi,Email,MaChucVu")] NhanVien nhanVien)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(nhanVien);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Employee));
        //    }
        //    ViewData["MaChucVu"] = new SelectList(_context.ChucVus, "MaChucVu", "TenChucVu", nhanVien.MaChucVu);
        //    return View(nhanVien);
        //}
        //public IActionResult AddEmployee(NhanVien nhanVien)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.NhanViens.Add(nhanVien);
        //        _context.SaveChanges();
        //        return RedirectToAction(nameof(Employee));
        //    }
        //    return View(nhanVien);
        //}


        // Phương thức hiển thị danh sách nhân viên
        public IActionResult Employee()
        {
            // Sử dụng Include để tải thông tin ChucVu liên quan
            var employees = _context.NhanViens.Include(nv => nv.MaChucVuNavigation).ToList();
            if (employees == null || !employees.Any())
            {
                // Xử lý trường hợp không có nhân viên
                employees = new List<NhanVien>(); // Tạo một danh sách rỗng để tránh lỗi NullReferenceException
            }
            return View(employees); // Truyền danh sách nhân viên đến View
        }

    }
}
