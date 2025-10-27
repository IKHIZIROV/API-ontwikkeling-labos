
using Labo02_RestaurantGetPost.Services;
using Labo02_RestaurantServiceDI.Services;

namespace Labo02_RestaurantServiceDI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddSingleton<IRestaurantService, RestaurantService>();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
