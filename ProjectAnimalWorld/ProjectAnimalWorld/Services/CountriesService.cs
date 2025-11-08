using ProjectAnimalWorld.Models;
using System.Collections.Concurrent;

namespace ProjectAnimalWorld.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly ConcurrentDictionary<int, Country> _countries = new();
        private int _nextId = 1;

        public CountriesService()
        {
            _countries[_nextId] = new Country { Id = _nextId++, Name = "Kenya", ContinentId = 1 };
            _countries[_nextId] = new Country { Id = _nextId++, Name = "South Africa", ContinentId = 1 };
            _countries[_nextId] = new Country { Id = _nextId++, Name = "Belgium", ContinentId = 2 };
            _countries[_nextId] = new Country { Id = _nextId++, Name = "Japan", ContinentId = 3 };
        }

        public Task<List<Country>> GetAllCountries()
            => Task.FromResult(_countries.Values.ToList());

        public Task<Country?> GetCountryById(int id)
        {
            _countries.TryGetValue(id, out var country);
            return Task.FromResult(country);
        }

        public Task<List<Country>> GetCountriesByContinent(int continentId)
        {
            var list = _countries.Values.Where(c => c.ContinentId == continentId).ToList();
            return Task.FromResult(list);
        }

        public Task CreateCountry(Country country)
        {
            country.Id = _nextId++;
            _countries[country.Id] = country;
            return Task.CompletedTask;
        }

        public Task<Country?> UpdateCountry(int id, Country country)
        {
            if (!_countries.ContainsKey(id))
                return Task.FromResult<Country?>(null);

            country.Id = id;
            _countries[id] = country;
            return Task.FromResult<Country?>(country);
        }

        public Task DeleteCountry(int id)
        {
            _countries.TryRemove(id, out _);
            return Task.CompletedTask;
        }
    }
}
