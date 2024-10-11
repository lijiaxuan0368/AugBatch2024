using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options) 
        {
            // DbSet  
        }

        public DbSet<Genre> Genres { get; set; }

        // migration
    }
}
