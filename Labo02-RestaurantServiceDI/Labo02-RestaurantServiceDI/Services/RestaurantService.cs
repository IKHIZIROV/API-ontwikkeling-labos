using Labo02_RestaurantServiceDI.Models;
using Labo02_RestaurantServiceDI.Services;
using Microsoft.VisualBasic;
using System.Runtime.ExceptionServices;

namespace Labo02_RestaurantGetPost.Services
{
    public class RestaurantService : IRestaurantService
    {
        private static readonly List<Restaurant> AllRestaurants = new();

        public Task CreateRestaurant(Restaurant item)
        {
            AllRestaurants.Add(item);
            return Task.CompletedTask;
        }

        public Task<Restaurant?> GetRestaurant(int id)
        {
            return Task.FromResult(AllRestaurants.FirstOrDefault(x => x.Id == id));
        }

        public Task<List<Restaurant>> GetAllRestaurants()
        {
            return Task.FromResult(AllRestaurants);
        }

        public Task<Restaurant?> UpdateRestaurant(int id, Restaurant item)
        {
            var Restaurant = AllRestaurants.FirstOrDefault(x => x.Id == id);
            if (Restaurant != null)
            {
                Restaurant.Id = item.Id;
                Restaurant.Name = item.Name;
                Restaurant.CuisineType = item.CuisineType;
                Restaurant.Rating = item.Rating;
                Restaurant.Location = item.Location;
            }

            return Task.FromResult(Restaurant);
        }

        public Task DeleteRestaurant(int id)
        {
            var Restaurant = AllRestaurants.FirstOrDefault(x => x.Id == id);
            if (Restaurant != null)
            {
                AllRestaurants.Remove(Restaurant);
            }

            return Task.CompletedTask;
        }
    }
}
