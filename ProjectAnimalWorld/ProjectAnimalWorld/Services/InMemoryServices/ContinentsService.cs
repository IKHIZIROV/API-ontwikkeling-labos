using ProjectAnimalWorld.Models;
using ProjectAnimalWorld.Services.Interfaces;
using System.Collections.Concurrent;

namespace ProjectAnimalWorld.Services.InMemoryServices
{
    public class ContinentsService : IContinentsService
    {
        private readonly ConcurrentDictionary<int, Continent> _continents = new();
        private int _nextId = 1;

        public ContinentsService()
        {
            _continents[_nextId] = new Continent { Id = _nextId++, Name = "Africa" };
            _continents[_nextId] = new Continent { Id = _nextId++, Name = "Europe" };
            _continents[_nextId] = new Continent { Id = _nextId++, Name = "Asia" };
        }

        public Task<List<Continent>> GetAllContinents()
            => Task.FromResult(_continents.Values.ToList());

        public Task<Continent?> GetContinentById(int id)
        {
            _continents.TryGetValue(id, out var continent);
            return Task.FromResult(continent);
        }

        public Task CreateContinent(Continent continent)
        {
            continent.Id = _nextId++;
            _continents[continent.Id] = continent;
            return Task.CompletedTask;
        }

        public Task<Continent?> UpdateContinent(int id, Continent continent)
        {
            if (!_continents.ContainsKey(id))
                return Task.FromResult<Continent?>(null);

            continent.Id = id;
            _continents[id] = continent;
            return Task.FromResult<Continent?>(continent);
        }

        public Task DeleteContinent(int id)
        {
            _continents.TryRemove(id, out _);
            return Task.CompletedTask;
        }
    }
}
