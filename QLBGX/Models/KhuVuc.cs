using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class KhuVuc
{
    public int MaKhuVuc { get; set; }

    public string TenKhuVuc { get; set; } = null!;

    public int SucChua { get; set; }

    public virtual ICollection<ChoDoXe> ChoDoXes { get; set; } = new List<ChoDoXe>();
}
