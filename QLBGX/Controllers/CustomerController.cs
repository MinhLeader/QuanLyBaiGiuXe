using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBGX.Models; // Nhớ đảm bảo namespace này khớp với namespace của bạn
using System.Linq;

namespace QLBGX.Controllers
{
    public class CustomerController : Controller // Đặt tên class Controller theo chuẩn là CustomerController
    {
        private readonly QuanLyBaiGiuXeContext _context; // Giả định rằng bạn có một DbContext tên là DbContext

        public CustomerController(QuanLyBaiGiuXeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ShowCustomer()
        {
            var danhSachKhachHang = await _context.Set<KhachHang>().ToListAsync();
            return View(danhSachKhachHang);
        }

        // Phương thức GET: Customer/Delete/5
        public async Task<IActionResult> DeleteCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // Phương thức POST: Customer/Delete/5
        [HttpPost, ActionName("DeleteCustomer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang != null)
            {
                _context.KhachHangs.Remove(khachHang);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ShowCustomer));
        }
        // Phương thức GET: Customer/Edit/5
        public async Task<IActionResult> EditCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        // Phương thức POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(int id, KhachHang khachHang)
        {
            if (id != khachHang.MaKh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachHangExists(khachHang.MaKh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ShowCustomer));
            }
            return View(khachHang);
        }

        private bool KhachHangExists(int id)
        {
            return _context.KhachHangs.Any(e => e.MaKh == id);
        }

        // Phương thức GET: Customer/Details/5
        public async Task<IActionResult> DetailsCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }


        // Phương thức GET: Customer/Create
        public IActionResult CreateCustomer()
        {
            return View();
        }

        // Phương thức POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCustomer(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khachHang);
                _context.SaveChanges();
                return RedirectToAction(nameof(ShowCustomer));
            }
            return View(khachHang);
        }


    }

}
