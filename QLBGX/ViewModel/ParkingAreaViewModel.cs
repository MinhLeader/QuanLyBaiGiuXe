// ParkingAreaViewModel.cs trong thư mục ViewModels
using QLBGX.Models;

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
    }
}
