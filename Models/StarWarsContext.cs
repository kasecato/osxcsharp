using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using StarWars.ViewModels;

namespace StarWars.Models
{
    public class StarWarsContext : DbContext
    {
        public StarWarsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Director> Directors { get; set; }

    }
}
