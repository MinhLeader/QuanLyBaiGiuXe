using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBGX.Models; // Nhớ đảm bảo namespace này khớp với namespace của bạn
using QLBGX.ViewModel;
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
                .Include(kh => kh.TaiKhoans)
                .Include(kh => kh.VeGuiXes)
                    .ThenInclude(ve => ve.LichSuGiaoDiches)
                        .ThenInclude(gd => gd.GiaoDichThanhToans)
                .AsNoTracking()
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
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var khachHang = await _context.KhachHangs
                        .Include(kh => kh.TaiKhoans)
                        .Include(kh => kh.VeGuiXes)
                            .ThenInclude(ve => ve.LichSuGiaoDiches)
                                .ThenInclude(gd => gd.GiaoDichThanhToans)
                        .FirstOrDefaultAsync(kh => kh.MaKh == id);

                    if (khachHang != null)
                    {
                        // Xóa các GiaoDichThanhToan liên quan
                        foreach (var ve in khachHang.VeGuiXes)
                        {
                            foreach (var giaoDich in ve.LichSuGiaoDiches)
                            {
                                _context.GiaoDichThanhToans.RemoveRange(giaoDich.GiaoDichThanhToans);
                            }
                            _context.LichSuGiaoDiches.RemoveRange(ve.LichSuGiaoDiches);
                        }

                        // Xóa các VeGuiXe liên quan
                        _context.VeGuiXes.RemoveRange(khachHang.VeGuiXes);

                        // Xóa các TaiKhoan liên quan
                        _context.TaiKhoans.RemoveRange(khachHang.TaiKhoans);

                        // Cuối cùng, xóa KhachHang
                        _context.KhachHangs.Remove(khachHang);
                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync();
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi và rollback nếu cần
                    await transaction.RollbackAsync();
                    // Log lỗi và/hoặc thông báo cho người dùng
                    // TODO: Add logging or user notification here
                }
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
      .Include(kh => kh.TaiKhoans) // Đảm bảo rằng bạn đã include thông tin tài khoản
      .FirstOrDefaultAsync(m => m.MaKh == id); // Sửa lại để sử dụng FirstOrDefaultAsync
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }


        // Phương thức GET: Customer/Create
        public IActionResult CreateCustomer()
        {
            // Tạo và truyền một instance của ViewModel mới vào View
            var viewModel = new CreateCustomerViewModel();
            return View(viewModel);
        }

        // Phương thức POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer(CreateCustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Tạo và thêm KhachHang mới
                var khachHang = new KhachHang
                {
                    TenKh = viewModel.TenKh,
                    SoDienThoai = viewModel.SoDienThoai,
                    DiaChi = viewModel.DiaChi,
                    Email = viewModel.Email
                };
                _context.KhachHangs.Add(khachHang);
                await _context.SaveChangesAsync();

                // Tạo và thêm TaiKhoan mới
                var taiKhoan = new TaiKhoan
                {
                    TenDangNhap = viewModel.TenDangNhap,
                    MatKhau = viewModel.MatKhau, // Mật khẩu nên được mã hóa
                    MaLoaiTaiKhoan = viewModel.MaLoaiTaiKhoan,
                    MaKh = khachHang.MaKh // Gán MaKh từ KhachHang vừa được thêm
                };
                _context.TaiKhoans.Add(taiKhoan);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(ShowCustomer));
            }
            return View(viewModel);
        }



    }

}
