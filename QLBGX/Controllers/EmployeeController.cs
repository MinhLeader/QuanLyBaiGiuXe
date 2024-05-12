﻿using Microsoft.AspNetCore.Mvc;
using QLBGX.Models;
using Microsoft.EntityFrameworkCore; // Thêm thư viện này để sử dụng Include
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage; // Cho giao dịch
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
        
        public async Task<IActionResult> Create([Bind("MaNv,TenNv,SoDienThoai,DiaChi,Email,MaChucVu")] NhanVien nhanVien )
        {
            // Retrieve the ChucVu entity based on the selected MaChucVu

            var selectedChucVu = _context.ChucVus.Find(nhanVien.MaChucVu);

            if (selectedChucVu != null)
            {
                nhanVien.MaChucVuNavigation = selectedChucVu;
            }
            ModelState.Clear();

            if (ModelState.IsValid)
            {
                _context.Add(nhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Employee));
            }
            else
            {
                // Prepare a general error message
                var errorMessage = "Lỗi khi thêm nhân viên. Vui lòng kiểm tra lại thông tin.";

                // Option 1: Pass error message directly to ViewData (simpler approach)
                ViewData["ErrorMessage"] = errorMessage;
            }

            if (TempData["PreviousPage"] != null)
            {
                return Redirect(TempData["PreviousPage"].ToString());
            }

            ViewData["MaChucVu"] = new SelectList(_context.ChucVus, "MaChucVu", "TenChucVu", nhanVien.MaChucVu);
            return View(nhanVien);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Lấy nhân viên với ChucVu liên quan để hiển thị chi tiết
            var nhanVien = await _context.NhanViens
                .Include(nv => nv.MaChucVuNavigation)
                .FirstOrDefaultAsync(m => m.MaNv == id);

            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhanVien = await _context.NhanViens
                        .Include(nv => nv.TaiKhoans)
                        .FirstOrDefaultAsync(nv => nv.MaNv == id);

            if (nhanVien != null)
            {
                // Xóa từng tài khoản liên kết với nhân viên
                foreach (var taiKhoan in nhanVien.TaiKhoans.ToList())
                {
                    _context.TaiKhoans.Remove(taiKhoan);
                }

                // Sau khi xóa tài khoản, xóa nhân viên
                _context.NhanViens.Remove(nhanVien);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Employee));
        }
      
        public async Task<IActionResult> Edit(int? id) 
    { 
        if (id == null) 
        { 
            return NotFound(); 
        } 

        var nhanVien = await _context.NhanViens.FindAsync(id); 
        if (nhanVien == null) 
        { 
            return NotFound(); 
        } 

        ViewData["MaChucVu"] = new SelectList(_context.ChucVus, "MaChucVu", "TenChucVu", nhanVien.MaChucVu); 
        return View(nhanVien); 
    }

    // POST: Employee/Edit/5 - Xử lý sửa thông tin nhân viên
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("MaNv,TenNv,SoDienThoai,DiaChi,Email,MaChucVu")] NhanVien nhanVienUpdated)
    {
        if (id != nhanVienUpdated.MaNv)
        {
            return NotFound();
        }
            ModelState.Clear();
            if (ModelState.IsValid)
        {
            try
            {
                var nhanVien = await _context.NhanViens.FindAsync(id);
                if (nhanVien == null)
                {
                    return NotFound();
                }

                nhanVien.TenNv = nhanVienUpdated.TenNv;
                nhanVien.SoDienThoai = nhanVienUpdated.SoDienThoai;
                nhanVien.DiaChi = nhanVienUpdated.DiaChi;
                nhanVien.Email = nhanVienUpdated.Email;
                nhanVien.MaChucVu = nhanVienUpdated.MaChucVu;

                _context.Update(nhanVien);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NhanVienExists(nhanVienUpdated.MaNv))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Employee)); // hoặc bạn có thể redirect đến trang chi tiết hoặc trang danh sách
        }

        ViewData["MaChucVu"] = new SelectList(_context.ChucVus, "MaChucVu", "TenChucVu", nhanVienUpdated.MaChucVu);
        return View(nhanVienUpdated);
    }

    private bool NhanVienExists(int id) 
    { 
        return _context.NhanViens.Any(e => e.MaNv == id); 
    } 
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
