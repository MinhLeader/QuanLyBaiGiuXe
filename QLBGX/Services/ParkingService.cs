// ParkingService.cs trong thư mục Services
using Microsoft.EntityFrameworkCore;
using QLBGX.Models;
using System.Collections.Generic;
using System.Linq;

namespace QLBGX.Services
{
    public class ParkingService : IParkingService
    {
        private readonly QuanLyBaiGiuXeContext _context;

        public ParkingService(QuanLyBaiGiuXeContext context)
        {
            _context = context;
        }

        public IEnumerable<ChoDoXe> GetAllParkingSpots()
        {
            // Logic để lấy tất cả chỗ đỗ xe
            return _context.ChoDoXes.Include(cdx => cdx.BienSoXeNavigation).ToList();
        }

        public IEnumerable<KhuVuc> GetAllAreas()
        {
            // Logic để lấy tất cả khu vực
            return _context.KhuVucs.ToList();
        }
    }
}
