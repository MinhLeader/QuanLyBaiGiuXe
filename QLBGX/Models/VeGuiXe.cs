using System;
using System.Collections.Generic;

namespace QLBGX.Models;

public partial class VeGuiXe
{
    public int MaVe { get; set; }

    public int MaLoaiVe { get; set; }

    public string BienSoXe { get; set; } = null!;

    public DateTime NgayGui { get; set; }

    public DateTime? NgayLay { get; set; }

    public int? MaKh { get; set; }

    public DateTime? NgayHetHan { get; set; }

    public int? SoGio { get; set; }

    public int? MaChoDoXe { get; set; }

    public virtual Xe BienSoXeNavigation { get; set; } = null!;

    public virtual ICollection<LichSuGiaoDich> LichSuGiaoDiches { get; set; } = new List<LichSuGiaoDich>();

    public virtual ChoDoXe? MaChoDoXeNavigation { get; set; }

    public virtual KhachHang? MaKhNavigation { get; set; }

    public virtual LoaiVe MaLoaiVeNavigation { get; set; } = null!;
}
