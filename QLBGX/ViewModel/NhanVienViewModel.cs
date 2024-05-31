using System.ComponentModel.DataAnnotations;

namespace QLBGX.Models
{
    public class NhanVienViewModel
    {
        public int MaNv { get; set; }

        [Required(ErrorMessage = "Tên nhân viên là bắt buộc")]
        public string TenNv { get; set; } = null!;

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        public string SoDienThoai { get; set; } = null!;

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        public string DiaChi { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Chức vụ là bắt buộc")]
        public int MaChucVu { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string TenDangNhap { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; } = null!;
    }
}
