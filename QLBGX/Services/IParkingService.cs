using QLBGX.Models;

namespace QLBGX.Services
{
    public interface IParkingService
    {
        IEnumerable<ChoDoXe> GetAllParkingSpots();
        IEnumerable<KhuVuc> GetAllAreas();
    }
}
