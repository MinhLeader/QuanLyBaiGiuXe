
using System.Collections.Generic;
using QLBGX.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
namespace QLBGX.ViewModel
{
    public class VeGuiXeViewModel
    {
        public int MaVe { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại vé")]
        public int MaLoaiVe { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập biển số xe")]
        public string BienSoXe { get; set; } = null!;

        public DateTime NgayGui { get; set; }

        public DateTime? NgayLay { get; set; }

        public int MaKh { get; set; }
        public string TenKh { get; set; }
        public string SoDienThoai { get; set; }

        // Các thuộc tính mới
        public string HieuXe { get; set; }
        public string Model { get; set; }
        public string LoaiXe { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số giờ gửi")]
        [Range(1, 24, ErrorMessage = "Số giờ gửi phải từ 1 đến 24")]
        public int SoGio { get; set; }
        public DateTime? NgayHetHan { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn khu vực")]
        public int MaKhuVuc { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn chỗ đỗ")]
        public string SoChoDoXe { get; set; } // Thay đổi kiểu dữ liệu thành string

        public IFormFile HinhAnhXe { get; set; }
        public List<KhuVuc> KhuVucs { get; set; } = new List<KhuVuc>();
        public List<LoaiVe> LoaiVes { get; set; } = new List<LoaiVe>();
        public List<SelectListItem> ChoDoXeList { get; set; } = new List<SelectListItem>();

    }
    
}
