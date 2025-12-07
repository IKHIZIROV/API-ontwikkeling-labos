using Microsoft.EntityFrameworkCore;
using ProjectAnimalWorld.Data;
using ProjectAnimalWorld.Models;
using ProjectAnimalWorld.Services.Interfaces;

namespace ProjectAnimalWorld.Services.DatabaseServices
{
    public class ContinentsDbService : IContinentsService
    {
        private readonly AnimalWorldContext _context;

        public ContinentsDbService(AnimalWorldContext context)
        {
            _context = context;
        }

        public async Task<List<Continent>> GetAllContinents()
        {
            return await _context.Continents.AsNoTracking().ToListAsync();
        }

        public async Task<Continent?> GetContinentById(int id)
        {
            return await _context.Continents
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateContinent(Continent continent)
        {
            await _context.Continents.AddAsync(continent);
            await _context.SaveChangesAsync();
        }

        public async Task<Continent?> UpdateContinent(int id, Continent continent)
        {
            var existing = await _context.Continents.FindAsync(id);
            if (existing == null) return null;

            existing.Name = continent.Name;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteContinent(int id)
        {
            var existing = await _context.Continents.FindAsync(id);
            if (existing == null) return;

            _context.Continents.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}
