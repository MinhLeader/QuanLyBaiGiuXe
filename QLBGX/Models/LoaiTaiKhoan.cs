using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class LoaiTaiKhoan
{
    public int MaLoaiTaiKhoan { get; set; }

    public string TenLoaiTaiKhoan { get; set; } = null!;

    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();
}
