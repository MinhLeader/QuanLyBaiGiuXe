using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class MauXe
{
    public int MaMauXe { get; set; }

    public string TenMau { get; set; } = null!;

    public virtual ICollection<Xe> Xes { get; set; } = new List<Xe>();
}
