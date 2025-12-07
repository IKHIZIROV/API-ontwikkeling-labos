using ProjectAnimalWorld.Models;

namespace ProjectAnimalWorld.Services.Interfaces
{
    public interface IAnimalsService
    {
        Task<List<Animal>> GetAllAnimals();
        Task<Animal?> GetAnimalById(int id);
        Task<List<Animal>> GetAnimalsByCountry(int countryId);
        Task<List<Animal>> SearchAnimalsByName(string name);
        Task CreateAnimal(Animal animal);
        Task<Animal?> UpdateAnimal(int id, Animal animal);
        Task DeleteAnimal(int id);
    }
}