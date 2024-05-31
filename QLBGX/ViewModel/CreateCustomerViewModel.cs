namespace QLBGX.ViewModel
{
    public class CreateCustomerViewModel
    {
        public string TenKh { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        // Mã loại tài khoản cho "Khách hàng" mặc định
        public int MaLoaiTaiKhoan { get; set; } = 2; // Giả sử mã cho "Khách hàng" là 2
    }
}
