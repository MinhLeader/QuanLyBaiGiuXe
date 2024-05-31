using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class ChoDoXe
{
    public int MaChoDoXe { get; set; }

    public int MaKhuVuc { get; set; }

    public int SoCho { get; set; }

    public string? BienSoXe { get; set; }

    public int MaTrangThai { get; set; }

    public virtual Xe? BienSoXeNavigation { get; set; }

    public virtual KhuVuc MaKhuVucNavigation { get; set; } = null!;

    public virtual TrangThaiChoDoXe MaTrangThaiNavigation { get; set; } = null!;

    public virtual ICollection<VeGuiXe> VeGuiXes { get; set; } = new List<VeGuiXe>();
}
