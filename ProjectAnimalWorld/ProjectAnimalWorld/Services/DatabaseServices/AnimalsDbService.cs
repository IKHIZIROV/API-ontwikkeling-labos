using Microsoft.EntityFrameworkCore;
using ProjectAnimalWorld.Data;
using ProjectAnimalWorld.Models;
using ProjectAnimalWorld.Services.Interfaces;

namespace AnimalWorldAPI.Services
{
    public class AnimalsDbService : IAnimalsService
    {
        private readonly AnimalWorldContext _context;

        public AnimalsDbService(AnimalWorldContext context)
        {
            _context = context;
        }

        public async Task<List<Animal>> GetAllAnimals()
        {
            return await _context.Animals.AsNoTracking().ToListAsync();
        }

        public async Task<Animal?> GetAnimalById(int id)
        {
            return await _context.Animals
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Animal>> GetAnimalsByCountry(int countryId)
        {
            return await _context.Animals
                .Where(a => a.CountryId == countryId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Animal>> SearchAnimalsByName(string name)
        {
            return await _context.Animals
                .Where(a => a.Name.Contains(name))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task CreateAnimal(Animal animal)
        {
            await _context.Animals.AddAsync(animal);
            await _context.SaveChangesAsync();
        }

        public async Task<Animal?> UpdateAnimal(int id, Animal animal)
        {
            var existing = await _context.Animals.FindAsync(id);
            if (existing == null) return null;

            existing.Name = animal.Name;
            existing.Species = animal.Species;
            existing.CountryId = animal.CountryId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteAnimal(int id)
        {
            var existing = await _context.Animals.FindAsync(id);
            if (existing == null) return;

            _context.Animals.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}
