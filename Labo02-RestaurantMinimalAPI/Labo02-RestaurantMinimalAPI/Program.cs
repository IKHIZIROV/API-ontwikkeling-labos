using Labo02_RestaurantMinimalAPI.Models;
using Labo02_RestaurantMinimalAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Labo02_RestaurantMinimalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddSingleton<IRestaurantService, RestaurantService>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapGet("/restaurants/{id}", async (IRestaurantService restaurantService, int id) =>
            {
                var restaurant = await restaurantService.GetRestaurant(id);
                return restaurant == null ? Results.NotFound() : Results.Ok(restaurant);

            }).WithName("GetRestaurant").WithOpenApi().WithTags("Restuarant");

            app.MapGet("/restaurants", async (IRestaurantService restaurantService) =>
            {
                var restaurants = await restaurantService.GetAllRestaurants();
                return Results.Ok(restaurants);

            }).WithName("GetRestaurants").WithOpenApi().WithTags("Restaurant");

            app.MapPost("/restaurants", async (IRestaurantService restaurantService, Restaurant item) =>
            {
                await restaurantService.CreateRestaurant(item);
                return Results.Created($"/restaurants/{item.Id}", item);

            }).WithName("CreateRestaurant").WithOpenApi().WithTags("Restaurant");

            app.MapPut("/restaurant/{id}", async (IRestaurantService restaurantService, int id, Restaurant item) =>
            {
                if ( id != item.Id)
                {
                    return Results.BadRequest();
                }

                var restaurant = await restaurantService.UpdateRestaurant(id, item);
                return restaurant == null ? Results.NotFound() : Results.Ok(restaurant);

            }).WithName("UpdateRestaurant").WithOpenApi().WithTags("Restaurant");

            app.MapDelete("/restaurant", async (IRestaurantService restaurantService, int id) =>
            {
                try
                {
                    await restaurantService.DeleteRestaurant(id);
                    return Results.NoContent();
                } catch(KeyNotFoundException)
                {
                    return Results.NotFound();
                }
            }).WithName("DeleteRestaurant").WithOpenApi().WithTags("Restaurant");

            app.Run();
        }
    }
}
