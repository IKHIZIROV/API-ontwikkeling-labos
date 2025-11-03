
namespace Labo03_Environments
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("/environment", (IHostEnvironment env, IConfiguration config) =>
            {
                var environmentMessage = config["AppSettings:EnvironmentMessage"];
                if (env.IsDevelopment())
                {
                    return Results.Ok(environmentMessage);
                }
                else if (env.IsProduction())
                {
                    return Results.Ok(environmentMessage);
                }
                else
                {
                    return Results.Ok("Onbekende omgeving.");
                }
            });

            app.Run();
        }
    }
}
