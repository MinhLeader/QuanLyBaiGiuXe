using Microsoft.AspNetCore.Mvc;
using QLBGX.Services;
using QLBGX.ViewModel;
using QLBGX.ViewModels;
using System.Linq;

namespace QLBGX.Controllers
{
    public class ParkingController : Controller
    {
        private readonly IParkingService _parkingService;

        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        // Hiển thị danh sách chỗ đỗ xe
        public IActionResult ShowParking()
        {
            var areas = _parkingService.GetAllAreas();
            var parkingSpots = _parkingService.GetAllParkingSpots();

            var areaViewModels = areas.Select(area => new ParkingAreaViewModel
            {
                Area = area,
                ParkingSpots = parkingSpots
                    .Where(ps => ps.MaKhuVuc == area.MaKhuVuc)
                    .Select(ps => new ChoDoXeViewModel
                    {
                        ParkingSpot = ps,
                        IsOccupied = !string.IsNullOrEmpty(ps.BienSoXe),
                        Vehicle = ps.BienSoXeNavigation,
                        TrangThai = ps.MaTrangThaiNavigation?.TenTrangThai ?? "Unknown" // Sử dụng null-coalescing operator để tránh lỗi null
                    }).ToList()
            }).ToList();

            return View(areaViewModels);
        }

        // Trả về partial view chứa các chỗ đỗ xe cho khu vực cụ thể
        public IActionResult GetParkingSpots(int areaId)
        {

            var parkingSpots = _parkingService.GetAllParkingSpots()
    .Where(ps => ps.MaKhuVuc == areaId)
    .Select(ps => new ChoDoXeViewModel
    {
        ParkingSpot = ps,
        IsOccupied = !string.IsNullOrEmpty(ps.BienSoXe),
        Vehicle = ps.BienSoXeNavigation,
        // Sử dụng null-coalescing operator để tránh lỗi null
        TrangThai = ps.MaTrangThaiNavigation?.TenTrangThai ?? "Không xác định"
    }).ToList();
            return PartialView("_ParkingSpots", parkingSpots);
        }
         // chi tiết
        public IActionResult GetParkingSpotDetails(int id)
        {
            var parkingSpot = _parkingService.GetParkingSpotById(id);
            if (parkingSpot == null)
            {
                return NotFound();
            }

            var viewModel = new ParkingSpotDetailsViewModel
            {
                ParkingSpot = parkingSpot,
                VehicleImage = parkingSpot.BienSoXeNavigation?.HinhAnh
            };

            return PartialView("_ParkingSpotDetails", viewModel);
        }

        //// tìm kiếm 
        [HttpGet]
        public IActionResult SearchParkingSpots(string searchTerm)
        {
            var areas = _parkingService.GetAllAreas();
            var parkingSpots = _parkingService.SearchParkingSpots(searchTerm);

            // Tạo một danh sách các MaChoDoXe phù hợp với kết quả tìm kiếm
            var matchingSpotIds = parkingSpots.Select(ps => ps.MaChoDoXe).ToList();

            var areaViewModels = areas.Select(area => new ParkingAreaViewModel
            {
                Area = area,
                ParkingSpots = parkingSpots
                    .Where(ps => ps.MaKhuVuc == area.MaKhuVuc)
                    .Select(ps => new ChoDoXeViewModel
                    {
                        ParkingSpot = ps,
                        IsOccupied = !string.IsNullOrEmpty(ps.BienSoXe),
                        Vehicle = ps.BienSoXeNavigation,
                        TrangThai = ps.MaTrangThaiNavigation?.TenTrangThai,
                        IsMatchingSearch = matchingSpotIds.Contains(ps.MaChoDoXe) // Thêm thuộc tính này
                    }).ToList()
            }).ToList();

            return PartialView("_ParkingSpots", areaViewModels);
        }






    }
}
