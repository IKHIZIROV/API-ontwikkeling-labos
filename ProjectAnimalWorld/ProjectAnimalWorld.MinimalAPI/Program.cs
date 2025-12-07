using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProjectAnimalWorld.Data;
using ProjectAnimalWorld.Models;
using System.Diagnostics.Metrics;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------
// DATABASE CONFIGURATION
// --------------------------------------------

var connectionString = "server=localhost;port=3306;database=api;user=root;password=root";

builder.Services.AddDbContext<AnimalWorldContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


// --------------------------------------------
// SWAGGER CONFIGURATION
// --------------------------------------------

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AnimalWorld Minimal API",
        Version = "v1",
        Description = "A minimal API version of the Animal World project."
    });
});


// --------------------------------------------
// BUILD APP
// --------------------------------------------

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnimalWorld Minimal API v1");
        c.RoutePrefix = "swagger";  // swagger at /swagger
    });
}


// --------------------------------------------
// ENDPOINTS
// --------------------------------------------

// Root
app.MapGet("/", () => "Minimal API for AnimalWorld API")
   .WithTags("Root");

// ----------------------------------------------
// CONTINENTS
// ----------------------------------------------

app.MapGet("/continents", async (AnimalWorldContext db) =>
{
    return Results.Ok(await db.Continents.ToListAsync());
})
.WithTags("Continents");

app.MapGet("/continents/{id:int}", async (int id, AnimalWorldContext db) =>
{
    var item = await db.Continents.FindAsync(id);
    return item is null ? Results.NotFound() : Results.Ok(item);
})
.WithTags("Continents");

app.MapPost("/continents", async (Continent continent, AnimalWorldContext db) =>
{
    db.Continents.Add(continent);
    await db.SaveChangesAsync();
    return Results.Created($"/continents/{continent.Id}", continent);
})
.WithTags("Continents");

app.MapPut("/continents/{id:int}", async (int id, Continent updated, AnimalWorldContext db) =>
{
    var continent = await db.Continents.FindAsync(id);
    if (continent is null) return Results.NotFound();

    continent.Name = updated.Name;
    await db.SaveChangesAsync();

    return Results.Ok(continent);
})
.WithTags("Continents");

app.MapDelete("/continents/{id:int}", async (int id, AnimalWorldContext db) =>
{
    var continent = await db.Continents.FindAsync(id);
    if (continent is null) return Results.NotFound();

    db.Continents.Remove(continent);
    await db.SaveChangesAsync();

    return Results.NoContent();
})
.WithTags("Continents");


// ----------------------------------------------
// COUNTRIES
// ----------------------------------------------

app.MapGet("/countries", async (AnimalWorldContext db) =>
{
    return Results.Ok(await db.Countries.ToListAsync());
})
.WithTags("Countries");

app.MapGet("/countries/{id:int}", async (int id, AnimalWorldContext db) =>
{
    var item = await db.Countries.FindAsync(id);
    return item is null ? Results.NotFound() : Results.Ok(item);
})
.WithTags("Countries");

app.MapPost("/countries", async (Country country, AnimalWorldContext db) =>
{
    db.Countries.Add(country);
    await db.SaveChangesAsync();
    return Results.Created($"/countries/{country.Id}", country);
})
.WithTags("Countries");

app.MapPut("/countries/{id:int}", async (int id, Country updated, AnimalWorldContext db) =>
{
    var country = await db.Countries.FindAsync(id);
    if (country is null) return Results.NotFound();

    country.Name = updated.Name;
    country.ContinentId = updated.ContinentId;
    await db.SaveChangesAsync();

    return Results.Ok(country);
})
.WithTags("Countries");

app.MapDelete("/countries/{id:int}", async (int id, AnimalWorldContext db) =>
{
    var country = await db.Countries.FindAsync(id);
    if (country is null) return Results.NotFound();

    db.Countries.Remove(country);
    await db.SaveChangesAsync();

    return Results.NoContent();
})
.WithTags("Countries");


// ----------------------------------------------
// ANIMALS
// ----------------------------------------------

app.MapGet("/animals", async (AnimalWorldContext db) =>
{
    return Results.Ok(await db.Animals.ToListAsync());
})
.WithTags("Animals");

app.MapGet("/animals/{id:int}", async (int id, AnimalWorldContext db) =>
{
    var item = await db.Animals.FindAsync(id);
    return item is null ? Results.NotFound() : Results.Ok(item);
})
.WithTags("Animals");

app.MapPost("/animals", async (Animal animal, AnimalWorldContext db) =>
{
    db.Animals.Add(animal);
    await db.SaveChangesAsync();
    return Results.Created($"/animals/{animal.Id}", animal);
})
.WithTags("Animals");

app.MapPut("/animals/{id:int}", async (int id, Animal updated, AnimalWorldContext db) =>
{
    var animal = await db.Animals.FindAsync(id);
    if (animal is null) return Results.NotFound();

    animal.Name = updated.Name;
    animal.Species = updated.Species;
    animal.CountryId = updated.CountryId;

    await db.SaveChangesAsync();

    return Results.Ok(animal);
})
.WithTags("Animals");

app.MapDelete("/animals/{id:int}", async (int id, AnimalWorldContext db) =>
{
    var animal = await db.Animals.FindAsync(id);
    if (animal is null) return Results.NotFound();

    db.Animals.Remove(animal);
    await db.SaveChangesAsync();

    return Results.NoContent();
})
.WithTags("Animals");


// ----------------------------------------------

app.Run();
