using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;
using StarWars.Model;

namespace StarWars.Domain
{
    public class StarWarsContext : DbContext
    {
        public DbSet<Director> Directors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = PlatformServices.Default.Application.ApplicationBasePath;
            optionsBuilder.UseNpgsql("Host=localhost;Username=c3po;Password=r2d2;Database=starwars");
        }
    }
}