using Microsoft.EntityFrameworkCore;
using ProjectAnimalWorld.Data;
using ProjectAnimalWorld.Models;
using ProjectAnimalWorld.Services.Interfaces;

namespace ProjectAnimalWorld.Services.DatabaseServices
{
    public class CountriesDbService : ICountriesService
    {
        private readonly AnimalWorldContext _context;

        public CountriesDbService(AnimalWorldContext context)
        {
            _context = context;
        }

        public async Task<List<Country>> GetAllCountries()
        {
            return await _context.Countries.AsNoTracking().ToListAsync();
        }

        public async Task<Country?> GetCountryById(int id)
        {
            return await _context.Countries
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Country>> GetCountriesByContinent(int continentId)
        {
            return await _context.Countries
                .Where(c => c.ContinentId == continentId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task CreateCountry(Country country)
        {
            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
        }

        public async Task<Country?> UpdateCountry(int id, Country country)
        {
            var existing = await _context.Countries.FindAsync(id);
            if (existing == null) return null;

            existing.Name = country.Name;
            existing.ContinentId = country.ContinentId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteCountry(int id)
        {
            var existing = await _context.Countries.FindAsync(id);
            if (existing == null) return;

            _context.Countries.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}
