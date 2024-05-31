using System.ComponentModel.DataAnnotations;

namespace QLBGX.ViewModel
{
    // LoginViewModel.cs

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string MatKhau { get; set; }
    }

}
