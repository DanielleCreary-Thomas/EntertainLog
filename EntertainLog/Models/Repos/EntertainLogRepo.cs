
using EntertainLog.Models.Database;

namespace EntertainLog.Models.Repos
{
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
            throw new NotImplementedException();
        }

        public Music AddMusic(Music music)
        {
            _dbContext.MusicSet.Add(music);
            _dbContext.SaveChanges();
            return music;
        }
    
        public TVShow AddTVShow(TVShow movie)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        //Delete
        public void DeleteBook(Book book)
        {
            _dbContext.BooksSet.Remove(book);
            _dbContext.SaveChanges();
        }

        public void DeleteMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public void DeleteMusic(Music music)
        {
            _dbContext.MusicSet.Remove(music);
            _dbContext.SaveChanges();
        }

        public void DeleteTVShow(TVShow tvshow)
        {
            throw new NotImplementedException();
        }

        //Get{id}
        public async Task<Book?> GetBookByIDAsync(long id)
        {
            return await _dbContext.BooksSet.FindAsync(id);
        }

        public Task<Movie?> GetMovieByIDAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Music?> GetMusicByIDAsync(long id)
        {
            return await _dbContext.MusicSet.FindAsync(id);
        }

        public Task<TVShow?> GetTVShowByIDAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByIDAsync(long id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Music UpdateMusic(Music music)
        {
            _dbContext.MusicSet.Update(music);
            _dbContext.SaveChanges();
            return music;
        }

        public TVShow UpdateTVShow(TVShow movie)
        {
            throw new NotImplementedException();
        }
    }
}
