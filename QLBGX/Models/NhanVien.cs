using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class NhanVien
{
    public int MaNv { get; set; }

    public string TenNv { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string? Email { get; set; }

    public int MaChucVu { get; set; }

    public virtual ChucVu MaChucVuNavigation { get; set; } = null!;

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
