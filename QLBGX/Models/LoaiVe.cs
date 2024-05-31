using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class LoaiVe
{
    public int MaLoaiVe { get; set; }

    public string TenLoaiVe { get; set; } = null!;

    public decimal GiaVe { get; set; }

    public decimal? GiaVeGio { get; set; }

    public virtual ICollection<VeGuiXe> VeGuiXes { get; set; } = new List<VeGuiXe>();
}
