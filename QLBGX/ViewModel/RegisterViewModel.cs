using System.ComponentModel.DataAnnotations;

namespace QLBGX.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("MatKhau", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string ConfirmMatKhau { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Tên khách hàng")]
        public string TenKH { get; set; }

        [Required]
        [Display(Name = "Số điện thoại")]
        public string SoDienThoai { get; set; }

        [Required]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }
    }
}
