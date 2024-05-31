using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class TaiKhoan
{
    public int MaTaiKhoan { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public int MaLoaiTaiKhoan { get; set; }

    public int? MaKh { get; set; }

    public int? MaNv { get; set; }

    public virtual KhachHang? MaKhNavigation { get; set; }

    public virtual LoaiTaiKhoan MaLoaiTaiKhoanNavigation { get; set; } = null!;

    public virtual NhanVien? MaNvNavigation { get; set; }
}
