
using EntertainLog.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace EntertainLog.Models.Repos
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
    /// The implements the interface of the same name
    /// </summary>
    public class EntertainLogRepo : IEntertainLogRepo
    {
        private EntertainLogDBContext _dbContext;
        public EntertainLogRepo(){ }
        public EntertainLogRepo(EntertainLogDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<User> Users => _dbContext.UsersSet;

        public IQueryable<Movie> Movies => _dbContext.MoviesSet;

        public IQueryable<Music> Musics => _dbContext.MusicSet;

        public IQueryable<Book> Books => _dbContext.BooksSet;

        public IQueryable<TVShow> TVShows => _dbContext.TVShowsSet;

        //Create
        public Book AddBook(Book book)
        {
            _dbContext.BooksSet.Add(book);
            _dbContext.SaveChanges();
            return book;
        }

        public Movie AddMovie(Movie movie)
        {
            _dbContext.MoviesSet.Add(movie);
            _dbContext.SaveChanges();
            return movie;
        }

        public Music AddMusic(Music music)
        {
            _dbContext.MusicSet.Add(music);
            _dbContext.SaveChanges();
            return music;
        }
    
        public TVShow AddTVShow(TVShow tvshow)
        {
            _dbContext.TVShowsSet.Add(tvshow);
            _dbContext.SaveChanges();
            return tvshow;
        }

        public void AddUser(User user)
        {
            _dbContext.UsersSet.Add(user);
            _dbContext.SaveChanges();
            
        }

        //Delete
        public void DeleteBook(Book book)
        {
            _dbContext.BooksSet.Remove(book);
            _dbContext.SaveChanges();
        }

        public void DeleteMovie(Movie movie)
        {
            _dbContext.MoviesSet.Remove(movie);
            _dbContext.SaveChanges();
        }

        public void DeleteMusic(Music music)
        {
            _dbContext.MusicSet.Remove(music);
            _dbContext.SaveChanges();
        }

        public void DeleteTVShow(TVShow tvshow)
        {
            _dbContext.TVShowsSet.Remove(tvshow);
            _dbContext.SaveChanges();

        }

        //Get{id}
        public async Task<Book?> GetBookByIDAsync(long id)
        {
            return await _dbContext.BooksSet.FindAsync(id);
        }

        public async Task<Movie?> GetMovieByIDAsync(long id)
        {
            return await _dbContext.MoviesSet.FindAsync(id);
        }

        public async Task<Music?> GetMusicByIDAsync(long id)
        {
            return await _dbContext.MusicSet.FindAsync(id);
        }

        public async Task<TVShow?> GetTVShowByIDAsync(long id)
        {
            return await _dbContext.TVShowsSet.FindAsync(id);
        }

        public async Task<User?> GetUserByIDAsync(long id)
        {
            return await _dbContext.UsersSet.FindAsync(id);
        }

        //Update
        public Book UpdateBook(Book book)
        {
            _dbContext.BooksSet.Update(book);
            _dbContext.SaveChanges();
            return book;
        }

        public Movie UpdateMovie(Movie movie)
        {
            _dbContext.MoviesSet.Update(movie);
            _dbContext.SaveChanges();
            return movie;
        }

        public Music UpdateMusic(Music music)
        {
            _dbContext.MusicSet.Update(music);
            _dbContext.SaveChanges();
            return music;
        }

        public TVShow UpdateTVShow(TVShow tvshow)
        {
            _dbContext.TVShowsSet.Update(tvshow);
            _dbContext.SaveChanges();
            return tvshow;
        }
    }
}
