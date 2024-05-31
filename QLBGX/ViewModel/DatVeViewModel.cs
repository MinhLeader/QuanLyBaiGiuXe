// File: ViewModel/DatVeViewModel.cs
using Microsoft.AspNetCore.Http;
using QLBGX.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLBGX.ViewModel
{
    /// <summary>
    /// / dụt được
    /// </summary>
    public class DatVeViewModel
    {
        public int MaLoaiVe { get; set; }
        public string BienSoXe { get; set; }
        public int? MaKh { get; set; }
        public int MaChoDoXe { get; set; }
        [Required(ErrorMessage = "Hiệu xe là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Hiệu xe không được vượt quá 50 ký tự.")]


        public string HieuXe { get; set; }

        [Required(ErrorMessage = "Loại xe là bắt buộc.")]
        public string LoaiXe { get; set; }

        [Required(ErrorMessage = "Model xe là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Model xe không được vượt quá 50 ký tự.")]
        public string Model { get; set; }


        [Required]
        public IFormFile? HinhAnh { get; set; }

        [Required]
        public List<LoaiVe> LoaiVes { get; set; }

        [Required]
        public List<KhachHang> KhachHangs { get; set; }

        [Required]
        public List<ChoDoXe> ChoDoXes { get; set; }

        [Required]
        public List<KhuVuc> KhuVucs { get; set; }

        [Required]
        public List<TrangThaiChoDoXe> TrangThaiChoDoXes { get; set; }
    }
}