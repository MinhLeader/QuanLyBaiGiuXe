using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class GiaoDichThanhToan
{
    public int MaGiaoDichThanhToan { get; set; }

    public int MaGiaoDich { get; set; }

    public int MaPhuongTien { get; set; }

    public decimal SoTien { get; set; }

    public virtual LichSuGiaoDich MaGiaoDichNavigation { get; set; } = null!;

    public virtual PhuongTienThanhToan MaPhuongTienNavigation { get; set; } = null!;
}
