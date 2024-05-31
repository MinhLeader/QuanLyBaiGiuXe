using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class TrangThaiChoDoXe
{
    public int MaTrangThai { get; set; }

    public string TenTrangThai { get; set; } = null!;

    public virtual ICollection<ChoDoXe> ChoDoXes { get; set; } = new List<ChoDoXe>();
}
