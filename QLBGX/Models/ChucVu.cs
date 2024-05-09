using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class ChucVu
{
    public int MaChucVu { get; set; }

    public string TenChucVu { get; set; } = null!;

    public virtual ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
