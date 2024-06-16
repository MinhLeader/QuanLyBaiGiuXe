using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class Xe
{
    public string BienSoXe { get; set; } = null!;

    public int MaMauXe { get; set; } = 5;

    public string HieuXe { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string LoaiXe { get; set; } = null!;

    public string? HinhAnh { get; set; }

    public virtual ICollection<ChoDoXe> ChoDoXes { get; set; } = new List<ChoDoXe>();

    public virtual MauXe MaMauXeNavigation { get; set; } = null!;

    public virtual ICollection<VeGuiXe> VeGuiXes { get; set; } = new List<VeGuiXe>();
}
