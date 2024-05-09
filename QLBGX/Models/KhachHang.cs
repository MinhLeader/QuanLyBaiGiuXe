using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class KhachHang
{
    public int MaKh { get; set; }

    public string TenKh { get; set; } = null!;

    public string SoDienThoai { get; set; } = null!;

    public string DiaChi { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<VeGuiXe> VeGuiXes { get; set; } = new List<VeGuiXe>();
}
