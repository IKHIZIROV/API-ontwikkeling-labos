using AnimalWorldAPI.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ProjectAnimalWorld.Data;
using ProjectAnimalWorld.Services.DatabaseServices;
using ProjectAnimalWorld.Services.InMemoryServices;
using ProjectAnimalWorld.Services.Interfaces;

namespace ProjectAnimalWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IContinentsService, ContinentsService>();
            builder.Services.AddSingleton<ICountriesService, CountriesService>();
            builder.Services.AddSingleton<IAnimalsService, AnimalsService>();

            var connectionString = "server=localhost;port=3310;database=animalworld-db;user=databanken;password=databanken";

            builder.Services.AddDbContext<AnimalWorldContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            bool useDatabase = true;

            if (useDatabase)
            {
                // MySql
                builder.Services.AddScoped<IAnimalsService, AnimalsDbService>();
                builder.Services.AddScoped<ICountriesService, CountriesDbService>();
                builder.Services.AddScoped<IContinentsService, ContinentsDbService>();
            }
            else
            {
                // in memory
                builder.Services.AddSingleton<IAnimalsService, AnimalsService>();
                builder.Services.AddSingleton<ICountriesService, CountriesService>();
                builder.Services.AddSingleton<IContinentsService, ContinentsService>();
            }

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseExceptionHandler("/error");

            app.Map("/error", (HttpContext context) =>
            {
                var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                return Results.Problem(
                    title: "An unexpected error occurred.",
                    detail: error?.Message
                );
            });

            app.Run();
        }
    }
}
