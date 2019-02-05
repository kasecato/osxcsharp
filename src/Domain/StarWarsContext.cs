using Microsoft.EntityFrameworkCore;
using StarWars.Model;

namespace StarWars.Domain
{
    public class StarWarsContext : DbContext
    {
        public DbSet<Director> Directors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=starwars;Port=15432;Username=c3po;Password=r2d2");
    }
}