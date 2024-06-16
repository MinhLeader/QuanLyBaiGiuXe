// File: Controllers/VeGuiXeController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLBGX.Models;
using QLBGX.ViewModel;
using System;
using System.IO;
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
                        MaKh = khachHang.MaKh,
                        TenKh = khachHang.TenKh,
                        SoDienThoai = khachHang.SoDienThoai,
                        KhuVucs = await _context.KhuVucs.ToListAsync(),
                        LoaiVes = await _context.LoaiVes.ToListAsync(),
                        ChoDoXeList = await _context.ChoDoXes
                        //    .Where(c => c.MaTrangThai == 1)
                            .Select(c => new SelectListItem
                            {
                                Value = c.MaChoDoXe.ToString(),
                                Text = $"{c.MaKhuVucNavigation.TenKhuVuc}-{c.SoCho}",
                                Disabled = c.MaTrangThai !=1
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
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                if (!int.TryParse(veGuiXeModel.SoChoDoXe, out int maChoDoXe))
                {
                    ModelState.AddModelError("SoChoDoXe", "Chỗ đỗ không hợp lệ.");
                    await LoadDropdownLists(veGuiXeModel);
                    return View(veGuiXeModel);
                }

                var choDoXe = await _context.ChoDoXes.FindAsync(maChoDoXe);
                if (choDoXe == null || choDoXe.MaTrangThai != 1)
                {
                    ModelState.AddModelError("SoChoDoXe", "Chỗ đỗ không hợp lệ hoặc đã có người đặt.");
                    await LoadDropdownLists(veGuiXeModel);
                    return View(veGuiXeModel);
                }

                var loaiVe = await _context.LoaiVes.FindAsync(veGuiXeModel.MaLoaiVe);
                var xe = await _context.Xes.FirstOrDefaultAsync(x => x.BienSoXe == veGuiXeModel.BienSoXe);
                if (xe == null)
                {
                    xe = new Xe { BienSoXe = veGuiXeModel.BienSoXe,
                        HieuXe = veGuiXeModel.HieuXe,
                        Model = veGuiXeModel.Model,
                        LoaiXe = veGuiXeModel.LoaiXe
                    };
                    _context.Xes.Add(xe);
                }

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
                        xe.HinhAnh = "" + uniqueFileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("HinhAnhXe", "Có lỗi xảy ra khi lưu hình ảnh: " + ex.Message);
                        await LoadDropdownLists(veGuiXeModel);
                        return View(veGuiXeModel);
                    }
                }

                var veGuiXe = new VeGuiXe
                {
                    MaLoaiVe = veGuiXeModel.MaLoaiVe,
                    BienSoXe = veGuiXeModel.BienSoXe,
                    NgayGui = DateTime.Now,
                    MaKh = veGuiXeModel.MaKh,
                    SoGio = veGuiXeModel.SoGio,
                    MaChoDoXe = maChoDoXe
                };

                veGuiXe.NgayHetHan = loaiVe.TenLoaiVe switch
                {
                    "Vé ngày" => veGuiXe.NgayGui.AddDays(1),
                    "Vé tháng" => veGuiXe.NgayGui.AddMonths(1),
                    "Vé giờ" => veGuiXe.NgayGui.AddHours(veGuiXeModel.SoGio),
                    _ => null
                };
                veGuiXe.TongTien = loaiVe.TenLoaiVe == "Vé giờ"
                    ? loaiVe.GiaVeGio.Value * veGuiXeModel.SoGio
                    : loaiVe.GiaVe;

                _context.VeGuiXes.Add(veGuiXe);
                await _context.SaveChangesAsync();

                choDoXe.MaTrangThai = 2;
                choDoXe.BienSoXe = veGuiXe.BienSoXe;
                await _context.SaveChangesAsync();

                return RedirectToAction("XacNhanDatVe", new { maVe = veGuiXe.MaVe });
            }

            await LoadDropdownLists(veGuiXeModel);
            return View(veGuiXeModel);
        }

        private async Task LoadDropdownLists(VeGuiXeViewModel veGuiXeModel)
        {
            veGuiXeModel.KhuVucs = await _context.KhuVucs.ToListAsync();
            veGuiXeModel.LoaiVes = await _context.LoaiVes.ToListAsync();
            veGuiXeModel.ChoDoXeList = await _context.ChoDoXes
                .Where(c => c.MaTrangThai == 1)
                .Select(c => new SelectListItem
                {
                    Value = c.MaChoDoXe.ToString(),
                    Text = $"{c.MaKhuVucNavigation.TenKhuVuc}-{c.SoCho}"
                })
                .ToListAsync();
        }


        public async Task<IActionResult> XacNhanDatVe(int maVe)
        {
            var veGuiXe = await _context.VeGuiXes
                .Include(v => v.MaLoaiVeNavigation)
                .Include(v => v.BienSoXeNavigation)
                .Include(v => v.MaChoDoXeNavigation)
                .Include(v => v.MaChoDoXeNavigation.MaKhuVucNavigation)
                .Include(v => v.MaKhNavigation) // Include khách hàng
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
        // Phương thức lấy chỗ đỗ theo khu vực
        [HttpGet]
        public async Task<IActionResult> LayChoDoTheoKhuVuc(int maKhuVuc)
        {
            var choDoXes = await _context.ChoDoXes
                .Where(c => c.MaKhuVuc == maKhuVuc /*&& c.MaTrangThai == 1*/)
                .Select(c => new
                {
                    c.MaChoDoXe,
                    SoCho = $"{c.MaKhuVucNavigation.TenKhuVuc}-{c.SoCho}",
                    MaTrangThai = c.MaTrangThai
                })
                .ToListAsync();

            return Json(choDoXes);
        }

        public string GetParkingSpotStatusClass(SelectListItem choDoXeItem)
        {
            if (choDoXeItem.Selected)
            {
                return "selected"; // Already selected by the user
            }

            int maTrangThai;
            if (int.TryParse(choDoXeItem.Value, out maTrangThai))
            {
                // Determine status based on MaTrangThai in your ChoDoXe entity
                return maTrangThai == 1 ? "available" : "occupied";
            }

            return "available"; // Default to available if parsing fails
        }

    }
}



