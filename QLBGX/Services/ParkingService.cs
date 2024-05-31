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

        public IEnumerable<KhuVuc> GetAllAreas()
        {
            return _context.KhuVucs.ToList();
        }

        public IEnumerable<ChoDoXe> GetAllParkingSpots()
        {
            return _context.ChoDoXes
             .Include(ps => ps.MaTrangThaiNavigation) // Bao gồm điều hướng đến trạng thái
             .Include(ps => ps.BienSoXeNavigation) // Bao gồm điều hướng đến xe
             .ToList();
             }

        public ChoDoXe GetParkingSpotById(int id)
        {
            return _context.ChoDoXes
        .Include(cd => cd.MaKhuVucNavigation)
        .Include(cd => cd.MaTrangThaiNavigation)
        .Include(cd => cd.BienSoXeNavigation)
        .FirstOrDefault(cd => cd.MaChoDoXe == id);
        }
        public IEnumerable<ChoDoXe> SearchParkingSpots(string searchTerm)
        {
            return _context.ChoDoXes
                .Include(ps => ps.BienSoXeNavigation)
                .Include(ps => ps.MaTrangThaiNavigation) // Chỉ include nếu cần
                .Where(ps => (ps.SoCho.ToString().Contains(searchTerm) ||
                              ps.BienSoXe.Contains(searchTerm) ||
                              ps.MaKhuVucNavigation.TenKhuVuc.Contains(searchTerm)) &&
                             ps.MaTrangThaiNavigation != null) // Kiểm tra null để tránh lỗi
                .ToList();
        }


    }
}
