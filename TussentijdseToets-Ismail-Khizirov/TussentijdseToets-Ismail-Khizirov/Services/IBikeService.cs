using TussentijdseToets_Ismail_Khizirov.Models;

namespace TussentijdseToets_Ismail_Khizirov.Services
{
    public interface IBikeService
    {
        public Task<List<Bike>> GetAllBikes();

        public Task<Bike?> GetBikeById(int id);

        public Task<List<Bike>> GetBikeByTypeAndLocation(string type, string location);
    }
}