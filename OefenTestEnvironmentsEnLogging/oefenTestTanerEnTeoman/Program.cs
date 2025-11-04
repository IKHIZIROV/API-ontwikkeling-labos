
using Serilog;
using Serilog.Formatting.Json;

namespace oefenTestTanerEnTeoman
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();

            var logger = new LoggerConfiguration()
            // Log naar tekstbestand in JSON-formaat
            .WriteTo.File(
                formatter: new JsonFormatter(),
                path: Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs/log.txt"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 90)
            // Log naar console
            .WriteTo.Console(new JsonFormatter())
            // Log naar Seq (start Seq lokaal!)
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();

            builder.Logging.AddSerilog(logger);

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



            app.Run();
        }
    }
}
