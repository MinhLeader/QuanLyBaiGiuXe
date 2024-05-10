using Microsoft.AspNetCore.Mvc;
using QLBGX.Models;
using System.Linq;
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

        // GET: Employee/AddEmployee
        public IActionResult AddEmployee()
        {
            // Tạo View Model nếu cần
            return View();
        }

        // POST: Employee/AddEmployee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee(NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                _context.NhanViens.Add(nhanVien);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(nhanVien);
        }

        // GET: Employee/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = _context.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            return View(nhanVien);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, NhanVien nhanVien)
        {
            if (id != nhanVien.MaNv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(nhanVien);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(nhanVien);
        }

        // GET: Employee/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = _context.NhanViens
                .FirstOrDefault(m => m.MaNv == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: Employee/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = _context.NhanViens
                .FirstOrDefault(m => m.MaNv == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var nhanVien = _context.NhanViens.Find(id);
            _context.NhanViens.Remove(nhanVien);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // Phương thức hiển thị danh sách nhân viên
        public IActionResult Employee()
        {
            var employees = _context.NhanViens.ToList(); // Lấy danh sách nhân viên từ database
            if (employees == null || !employees.Any())
            {
                // Xử lý trường hợp không có nhân viên
                employees = new List<NhanVien>(); // Tạo một danh sách rỗng để tránh lỗi NullReferenceException
            }
            return View(employees); // Truyền danh sách nhân viên đến View
        }

    }
}
