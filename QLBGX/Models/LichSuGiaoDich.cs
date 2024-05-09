using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class LichSuGiaoDich
{
    public int MaGiaoDich { get; set; }

    public DateTime ThoiGianGiaoDich { get; set; }

    public decimal TongTien { get; set; }

    public int MaVe { get; set; }

    public virtual ICollection<GiaoDichThanhToan> GiaoDichThanhToans { get; set; } = new List<GiaoDichThanhToan>();

    public virtual VeGuiXe MaVeNavigation { get; set; } = null!;
}
