using Microsoft.EntityFrameworkCore;
using ProjectAnimalWorld.Models;


namespace ProjectAnimalWorld.Data
{
    public class AnimalWorldContext : DbContext
    {
        public AnimalWorldContext(DbContextOptions<AnimalWorldContext> options) : base(options) { }

        public DbSet<Continent> Continents { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Animal> Animals { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "server=localhost;port=3306;database=api;user=root;password=root";
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
