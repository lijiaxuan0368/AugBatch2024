using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {
            // DbSet  
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuration
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<MovieGenre>(ConfigurationMovieGenre);
            //modelBuilder.Entity<Genre>();
        }

        private void ConfigurationMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre");
            builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            builder.HasOne(mg => mg.Movie).WithMany(g => g.MovieGenres).HasForeignKey(g => g.MovieId);
            builder.HasOne(mg => mg.Genre).WithMany(m => m.MovieGenres).HasForeignKey(m => m.GenreId);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder) {
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256); //[MaxLength(256)]
            builder.Property(m => m.Overview).HasMaxLength(4096);
            // TODO - Configuration 

            builder.Ignore(m => m.Rating);
        }

    }
}
