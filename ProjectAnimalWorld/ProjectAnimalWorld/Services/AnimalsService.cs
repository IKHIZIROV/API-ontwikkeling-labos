using ProjectAnimalWorld.Models;
using System.Collections.Concurrent;

namespace ProjectAnimalWorld.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly ConcurrentDictionary<int, Animal> _animals = new();
        private int _nextId = 1;

        public AnimalsService()
        {
            _animals[_nextId] = new Animal { Id = _nextId++, Name = "Lion", Species = "Panthera leo", CountryId = 2 };
            _animals[_nextId] = new Animal { Id = _nextId++, Name = "Elephant", Species = "Loxodonta africana", CountryId = 1 };
            _animals[_nextId] = new Animal { Id = _nextId++, Name = "Panda", Species = "Ailuropoda melanoleuca", CountryId = 4 };
        }

        public Task<List<Animal>> GetAllAnimals()
            => Task.FromResult(_animals.Values.ToList());

        public Task<Animal?> GetAnimalById(int id)
        {
            _animals.TryGetValue(id, out var animal);
            return Task.FromResult(animal);
        }

        public Task<List<Animal>> GetAnimalsByCountry(int countryId)
        {
            var list = _animals.Values.Where(a => a.CountryId == countryId).ToList();
            return Task.FromResult(list);
        }

        public Task<List<Animal>> SearchAnimalsByName(string name)
        {
            var list = _animals.Values
                .Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Task.FromResult(list);
        }

        public Task CreateAnimal(Animal animal)
        {
            animal.Id = _nextId++;
            _animals[animal.Id] = animal;
            return Task.CompletedTask;
        }

        public Task<Animal?> UpdateAnimal(int id, Animal animal)
        {
            if (!_animals.ContainsKey(id))
                return Task.FromResult<Animal?>(null);

            animal.Id = id;
            _animals[id] = animal;
            return Task.FromResult<Animal?>(animal);
        }

        public Task DeleteAnimal(int id)
        {
            _animals.TryRemove(id, out _);
            return Task.CompletedTask;
        }
    }
}
