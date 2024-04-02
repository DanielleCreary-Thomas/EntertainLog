
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
            throw new NotImplementedException();
        }

        public Movie AddMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Music AddMusic(Music music)
        {
            throw new NotImplementedException();
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
        public void DeleteBook(long id)
        {
            throw new NotImplementedException();
        }

        public void DeleteMovie(long id)
        {
            throw new NotImplementedException();
        }

        public void DeleteMusic(long id)
        {
            throw new NotImplementedException();
        }

        public void DeleteTVShow(long id)
        {
            throw new NotImplementedException();
        }

        //Get{id}
        public Task<Book?> GetBookByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie?> GetMovieByIDAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Music?> GetMusicByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<TVShow?> GetTVShowByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        //Update
        public Book UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Movie UpdateMovie(Movie movie)
        {
            throw new NotImplementedException();
        }

        public Music UpdateMusic(Music music)
        {
            throw new NotImplementedException();
        }

        public TVShow UpdateTVShow(TVShow movie)
        {
            throw new NotImplementedException();
        }
    }
}
