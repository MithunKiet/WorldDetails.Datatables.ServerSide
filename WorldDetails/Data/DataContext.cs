using Microsoft.EntityFrameworkCore;
using WorldDetails.Models.Entities;

namespace WorldDetails.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
