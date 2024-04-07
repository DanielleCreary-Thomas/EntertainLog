using Microsoft.EntityFrameworkCore;

namespace EntertainLog.Models.Database
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
    /// the Database context to hold the records of each table
    /// </summary>
    public class EntertainLogDBContext : DbContext
    {
        public EntertainLogDBContext(DbContextOptions<EntertainLogDBContext> option): base (option) { }

        public DbSet<User> UsersSet => Set<User>();
        
        public DbSet<Music> MusicSet => Set<Music>();
        public DbSet<Movie> MoviesSet => Set<Movie>();
        public DbSet<Book> BooksSet => Set<Book>();
        public DbSet<TVShow> TVShowsSet => Set<TVShow>();
    }
}
