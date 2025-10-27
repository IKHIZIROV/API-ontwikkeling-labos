using Labo02_RestaurantServiceDI.Models;

namespace Labo02_RestaurantServiceDI.Services
{
    public interface IRestaurantService
    {
        Task CreateRestaurant(Restaurant item);

        Task<Restaurant?> UpdateRestaurant(int id, Restaurant item);

        Task<Restaurant?> GetRestaurant(int id);

        Task<List<Restaurant>> GetAllRestaurants();

        Task DeleteRestaurant(int id);
    }
}
