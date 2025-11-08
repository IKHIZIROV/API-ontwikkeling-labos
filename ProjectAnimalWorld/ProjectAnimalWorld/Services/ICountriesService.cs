using ProjectAnimalWorld.Models;

namespace ProjectAnimalWorld.Services
{
    public interface ICountriesService
    {
        Task<List<Country>> GetAllCountries();
        Task<Country?> GetCountryById(int id);
        Task<List<Country>> GetCountriesByContinent(int continentId);
        Task CreateCountry(Country country);
        Task<Country?> UpdateCountry(int id, Country country);
        Task DeleteCountry(int id);
    }
}