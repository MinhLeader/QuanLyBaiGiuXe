using QLBGX.Models;
using System.Collections.Generic;
namespace QLBGX.Services
{
    public interface IParkingService
    {
        IEnumerable<KhuVuc> GetAllAreas();
        IEnumerable<ChoDoXe> GetAllParkingSpots();
        IEnumerable<ChoDoXe> SearchParkingSpots(string searchTerm);

        ChoDoXe GetParkingSpotById(int id);
    }
}
