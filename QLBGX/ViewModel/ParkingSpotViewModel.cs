// ParkingSpotViewModel.cs trong thư mục ViewModels
using QLBGX.Models;

namespace QLBGX.ViewModels
{
    public class ParkingSpotViewModel
    {

        // Thêm thuộc tính TrangThai vào đây nếu cần
         public ChoDoXe ParkingSpot { get; set; } // Đối tượng này chứa thông tin vị trí đỗ xe và trạng thái
        public Xe Vehicle { get; set; } // Đối tượng này chứa thông tin xe đỗ tại vị trí đó (nếu có)
        public IEnumerable<ChoDoXe> ParkingSpots { get; set; }
        public IEnumerable<KhuVuc> Areas { get; set; }
    }
}
