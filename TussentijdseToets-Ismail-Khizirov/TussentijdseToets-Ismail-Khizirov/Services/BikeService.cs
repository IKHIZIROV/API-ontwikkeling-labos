using System.Collections.Concurrent;
using TussentijdseToets_Ismail_Khizirov.Models;

namespace TussentijdseToets_Ismail_Khizirov.Services
{
    public class BikeService : IBikeService
    {
        private readonly ConcurrentDictionary<int, Bike> _bikes = new();
        private int _nextId = 1;

        public Task<List<Bike>> GetAllBikes()
        {
            var bikes = _bikes.Values.ToList();
            return Task.FromResult(bikes);
        }

        public Task<Bike?> GetBikeById(int id)
        {
            _bikes.TryGetValue(id, out var bike);
            return Task.FromResult(bike);
        }

        public Task<List<Bike>> GetBikeByTypeAndLocation(string type, string location)
        {
            var bikes = _bikes.Values
                .Where(p => p.Type?.Equals(type, StringComparison.OrdinalIgnoreCase) == true &&
                            p.Location?.Equals(location, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();
            return Task.FromResult(bikes);
        }
    }
}
