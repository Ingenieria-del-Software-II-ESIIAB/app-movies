using Microsoft.EntityFrameworkCore;

namespace AppForMovies.API.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = null!;
    }
}
