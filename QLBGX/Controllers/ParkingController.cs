using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBGX.Models; // Nhớ đảm bảo namespace này khớp với namespace của bạn
using System.Linq;
using QLBGX.Services;
using QLBGX.ViewModel;
using QLBGX.ViewModels;

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
                ParkingSpots = parkingSpots.Where(ps => ps.MaKhuVuc == area.MaKhuVuc)
                                           .Select(ps => new ChoDoXeViewModel
                                           {
                                               ParkingSpot = ps,
                                               IsOccupied = !string.IsNullOrEmpty(ps.BienSoXe),
                                               Vehicle = ps.BienSoXeNavigation // Không sử dụng null-coalescing operator ở đây
                                           })
            }).ToList();

            return View(areaViewModels);
        }
        // Thêm vào trong ParkingController.cs
        public IActionResult GetParkingSpots(int areaId)
        {
            var parkingSpots = _parkingService.GetAllParkingSpots()
                                              .Where(ps => ps.MaKhuVuc == areaId)
                                              .Select(ps => new ChoDoXeViewModel
                                              {
                                                  ParkingSpot = ps,
                                                  IsOccupied = !string.IsNullOrEmpty(ps.BienSoXe),
                                                  Vehicle = ps.BienSoXeNavigation
                                              }).ToList();
            return PartialView("_ParkingSpots", parkingSpots);
        }


    }
}
