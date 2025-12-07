using ProjectAnimalWorld.Models;

namespace ProjectAnimalWorld.Services.Interfaces
{
    public interface IContinentsService
    {
        Task<List<Continent>> GetAllContinents();
        Task<Continent?> GetContinentById(int id);
        Task CreateContinent(Continent continent);
        Task<Continent?> UpdateContinent(int id, Continent continent);
        Task DeleteContinent(int id);
    }
}