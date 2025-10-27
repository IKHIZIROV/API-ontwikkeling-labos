using Labo02_RestaurantGetPost.Services;
using Labo02_RestaurantServiceDI.Models;
using Labo02_RestaurantServiceDI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace Labo02_RestaurantGetPost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _RestaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _RestaurantService = restaurantService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var Restaurant = await _RestaurantService.GetRestaurant(id);
            if (Restaurant == null)
            {
                return NotFound();
            }

            return Ok(Restaurant);
        }

        [HttpGet]
        public async Task<ActionResult<List<Restaurant>>> GetAllRestaurants()
        {
            var Restaurants = await _RestaurantService.GetAllRestaurants();

            return Ok(Restaurants);
        }

        [HttpPost]
        public async Task<ActionResult<Restaurant>> CreateRestaurant(Restaurant item)
        {
            await _RestaurantService.CreateRestaurant(item);

            return CreatedAtAction(nameof(GetRestaurant), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Restaurant>> UpdateRestaurant(int id, Restaurant item)
        {
            if (id != item.Id)
            {
                return BadRequest(id);
            }

            var updatedRestaurant = await _RestaurantService.UpdateRestaurant(id, item);
            if (updatedRestaurant == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Restaurant>> DeleteRestaurant(int id)
        {
            var Restaurant = await _RestaurantService.GetRestaurant(id);
            if (Restaurant == null)
            {
                return NotFound();
            }

            await _RestaurantService.DeleteRestaurant(id);

            return Ok();
        }
    }
}
