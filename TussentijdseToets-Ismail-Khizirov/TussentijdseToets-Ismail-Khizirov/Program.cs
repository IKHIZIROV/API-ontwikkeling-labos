
using Serilog;
using Serilog.Formatting.Json;
using TussentijdseToets_Ismail_Khizirov.Services;

namespace TussentijdseToets_Ismail_Khizirov
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();

            var logger = new LoggerConfiguration()
            .WriteTo.File(
                formatter: new JsonFormatter(),
                path: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/log.txt"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 90)
            .WriteTo.Console(new JsonFormatter())
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();

            builder.Logging.AddSerilog(logger);

            builder.Services.AddSingleton<IBikeService, BikeService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.MapGet("/api/status", (IWebHostEnvironment env) =>
            {
                return new
                {
                    status = "BikeRental API operational",
                    environment = env.EnvironmentName,
                    timestamp = DateTime.Now
                };

            }).WithName("GetStatus").WithOpenApi().WithTags("Status");

            app.Run();
        }
    }
}
