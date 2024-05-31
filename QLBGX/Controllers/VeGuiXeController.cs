// File: Controllers/VeGuiXeController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBGX.Models;
using QLBGX.ViewModel;

using System.Linq;
using System.Security.Claims;

namespace QLBGX.Controllers
{
    [Authorize]
    public class VeGuiXeController : Controller
    {
        private readonly QuanLyBaiGiuXeContext _context;
        private readonly TaiKhoanService _taiKhoanService; // Khai báo biến

        private readonly IWebHostEnvironment _webHostEnvironment;

        public VeGuiXeController(QuanLyBaiGiuXeContext context, TaiKhoanService taiKhoanService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _taiKhoanService = taiKhoanService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> DatVe()
        {
            if (User.Identity.IsAuthenticated)
            {
                var khachHang = await _taiKhoanService.GetLoggedInUserAsync();
                if (khachHang != null)
                {
                    var veGuiXeModel = new VeGuiXeViewModel
                    {
                        MaKH = khachHang.MaKh,
                        TenKH = khachHang.TenKh,
                        SoDienThoai = khachHang.SoDienThoai,
                        KhuVucs = await _context.KhuVucs.ToListAsync(),
                        LoaiVes = await _context.LoaiVes.ToListAsync(),
                        // Lấy danh sách chỗ đỗ xe trống và đưa vào SelectListItem
                        ChoDoXes = await _context.ChoDoXes
                            .Where(c => c.MaTrangThai == 1) // Chỉ lấy các chỗ trống
                            .Select(c => new SelectListItem
                            {
                                Value = c.MaChoDoXe.ToString(), // Lưu trữ MaChoDoXe trong Value
                                Text = $"{c.MaKhuVucNavigation.TenKhuVuc}-{c.SoCho}" // Hiển thị số khu vực và số chỗ
                            })
                            .ToListAsync()
                    };
                    return View(veGuiXeModel);
                }
            }

            return RedirectToAction("Login", "Home");
        }



        [HttpPost]
        public async Task<IActionResult> DatVe(VeGuiXeViewModel veGuiXeModel, IFormFile hinhAnhXe)
        {
            if (ModelState.IsValid)
            {
                // Chuyển đổi SoChoDoXe (string) thành MaChoDoXe (int)
                if (!int.TryParse(veGuiXeModel.SoChoDoXe, out int maChoDoXe))
                {
                    ModelState.AddModelError("SoChoDoXe", "Chỗ đỗ không hợp lệ.");
                    veGuiXeModel.KhuVucs = await _context.KhuVucs.ToListAsync();
                    veGuiXeModel.LoaiVes = await _context.LoaiVes.ToListAsync();
                    veGuiXeModel.ChoDoXes = await _context.ChoDoXes
                        .Where(c => c.MaTrangThai == 1)
                        .Select(c => new SelectListItem
                        {
                            Value = c.MaChoDoXe.ToString(),
                            Text = $"{c.MaKhuVucNavigation.TenKhuVuc}-{c.SoCho}"
                        })
                        .ToListAsync();
                    return View(veGuiXeModel);
                }
                // Kiểm tra chỗ đỗ
                var choDoXe = await _context.ChoDoXes.FindAsync(maChoDoXe);
                if (choDoXe == null || choDoXe.MaTrangThai != 1)
                {
                    ModelState.AddModelError("SoChoDoXe", "Chỗ đỗ không hợp lệ hoặc đã có người đặt.");
                    veGuiXeModel.KhuVucs = await _context.KhuVucs.ToListAsync();
                    veGuiXeModel.LoaiVes = await _context.LoaiVes.ToListAsync();
                    veGuiXeModel.ChoDoXes = await _context.ChoDoXes
                        .Where(c => c.MaTrangThai == 1)
                        .Select(c => new SelectListItem
                        {
                            Value = c.MaChoDoXe.ToString(),
                            Text = $"{c.MaKhuVucNavigation.TenKhuVuc}-{c.SoCho}"
                        })
                        .ToListAsync();
                    return View(veGuiXeModel);
                }

                // Lấy thông tin loại vé
                var loaiVe = await _context.LoaiVes.FindAsync(veGuiXeModel.MaLoaiVe);

                // Tìm hoặc tạo bản ghi Xe dựa trên biển số xe
                var xe = await _context.Xes.FirstOrDefaultAsync(x => x.BienSoXe == veGuiXeModel.BienSoXe);
                if (xe == null)
                {
                    xe = new Xe { BienSoXe = veGuiXeModel.BienSoXe };
                    _context.Xes.Add(xe);
                }

                // Lưu hình ảnh (nếu có)
                if (hinhAnhXe != null && hinhAnhXe.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + hinhAnhXe.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    try
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await hinhAnhXe.CopyToAsync(fileStream);
                        }

                        xe.HinhAnh = "/assets/img/" + uniqueFileName; // Lưu đường dẫn vào thuộc tính HinhAnh của Xe
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("HinhAnhXe", "Có lỗi xảy ra khi lưu hình ảnh: " + ex.Message);
                        veGuiXeModel.KhuVucs = await _context.KhuVucs.ToListAsync();
                        veGuiXeModel.LoaiVes = await _context.LoaiVes.ToListAsync();
                        return View(veGuiXeModel); // Trả về View nếu có lỗi
                    }
                }

                // Tạo vé mới
                var veGuiXe = new VeGuiXe
                {
                    MaLoaiVe = veGuiXeModel.MaLoaiVe,
                    BienSoXe = veGuiXeModel.BienSoXe,
                    NgayGui = DateTime.Now,
                    MaKh = veGuiXeModel.MaKH,
                    SoGio = veGuiXeModel.SoGio,
                     MaChoDoXe = maChoDoXe

                };
                // Tính toán ngày hết hạn và giá vé
                veGuiXe.NgayHetHan = loaiVe.TenLoaiVe switch
                {
                    "Vé ngày" => veGuiXe.NgayGui.AddDays(1),
                    "Vé tháng" => veGuiXe.NgayGui.AddMonths(1),
                    "Vé giờ" => veGuiXe.NgayGui.AddHours(veGuiXeModel.SoGio),
                    _ => null
                };
                veGuiXe.GiaVe = loaiVe.TenLoaiVe == "Vé giờ"
                    ? loaiVe.GiaVeGio.Value * veGuiXeModel.SoGio
                    : loaiVe.GiaVe;

                _context.VeGuiXes.Add(veGuiXe);
                await _context.SaveChangesAsync();

                choDoXe.MaTrangThai = 2;
                choDoXe.BienSoXe = veGuiXe.BienSoXe;
                await _context.SaveChangesAsync();

                return RedirectToAction("XacNhanDatVe", new { maVe = veGuiXe.MaVe });
            }

            // Nếu có lỗi, nạp lại danh sách khu vực, loại vé, chỗ đỗ xe
            veGuiXeModel.KhuVucs = await _context.KhuVucs.ToListAsync();
            veGuiXeModel.LoaiVes = await _context.LoaiVes.ToListAsync();
            veGuiXeModel.ChoDoXes = await _context.ChoDoXes
                .Where(c => c.MaTrangThai == 1)
                .Select(c => new SelectListItem
                {
                    Value = c.MaChoDoXe.ToString(),
                    Text = $"{c.MaKhuVucNavigation.TenKhuVuc}-{c.SoCho}"
                })
                .ToListAsync();
            return View(veGuiXeModel);
        }

        public async Task<IActionResult> XacNhanDatVe(int maVe)
        {
            var veGuiXe = await _context.VeGuiXes
                .Include(v => v.MaLoaiVeNavigation)
                .Include(v => v.BienSoXeNavigation)
                .Include(v => v.MaChoDoXeNavigation)
                .Include(v => v.MaChoDoXeNavigation.MaKhuVucNavigation)
                .FirstOrDefaultAsync(v => v.MaVe == maVe);

            if (veGuiXe == null)
            {
                return NotFound();
            }

            var viewModel = new XacNhanDatVeViewModel
            {
                VeGuiXe = veGuiXe,
                 ChoDoXe = veGuiXe.MaChoDoXeNavigation
            };
            return View(viewModel);
        }

    }
}



