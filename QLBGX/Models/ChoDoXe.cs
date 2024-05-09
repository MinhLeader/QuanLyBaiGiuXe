using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class ChoDoXe
{
    public int MaChoDoXe { get; set; }

    public int MaKhuVuc { get; set; }

    public string BienSoXe { get; set; } = null!;

    public string LoaiXe { get; set; } = null!;

    public string TrangThai { get; set; } = null!;

    public virtual Xe BienSoXeNavigation { get; set; } = null!;

    public virtual KhuVuc MaKhuVucNavigation { get; set; } = null!;
}
