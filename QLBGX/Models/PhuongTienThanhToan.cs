using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class PhuongTienThanhToan
{
    public int MaPhuongTien { get; set; }

    public string TenPhuongTien { get; set; } = null!;

    public virtual ICollection<GiaoDichThanhToan> GiaoDichThanhToans { get; set; } = new List<GiaoDichThanhToan>();
}
