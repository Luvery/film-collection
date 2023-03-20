using FilmCollection.API.Controllers;
using Microsoft.EntityFrameworkCore;

namespace FilmCollection.API.Infrastructure
{
    public class FilmDbContext : DbContext
    {
        public FilmDbContext(DbContextOptions<FilmDbContext> options)
            : base (options)
        {

        }

        public DbSet<Film> Films { get; set; }
    }
}
