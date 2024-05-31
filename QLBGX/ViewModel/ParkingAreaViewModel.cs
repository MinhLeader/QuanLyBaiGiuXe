using QLBGX.Models;
using System.Collections.Generic;

namespace QLBGX.ViewModels
{
    public class ParkingAreaViewModel
    {
        public KhuVuc Area { get; set; }
        public IEnumerable<ChoDoXeViewModel> ParkingSpots { get; set; }
    }

    public class ChoDoXeViewModel
    {
        public ChoDoXe ParkingSpot { get; set; }
        public bool IsOccupied { get; set; }
        public Xe Vehicle { get; set; }
        public string TrangThai { get; set; } // Thêm thuộc tính này để chứa tên trạng thái
        public bool IsMatchingSearch { get; set; }
    }
}
