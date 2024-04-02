namespace EntertainLog.Models.Repos
{
    public interface IEntertainLogRepo
    {
        IQueryable<User> Users { get; }
        IQueryable<Movie> Movies { get; }
        IQueryable<Music> Musics { get; }
        IQueryable<Book> Books { get; }

        void AddUser(User user);
        Movie AddMovie(Movie movie);
        Music AddMusic(Music music);
        Book AddBook(Book book);
        TVShow AddTVShow(TVShow movie);

        Task<Movie?> GetMovieByIDAsync(long id);
        Task<Music?> GetMusicByIdAsync(long id);
        Task<Book?> GetBookByIdAsync(long id);
        Task<TVShow?> GetTVShowByIdAsync(long id);

        Movie UpdateMovie(Movie movie);
        Music UpdateMusic(Music music);
        Book UpdateBook(Book book);
        TVShow UpdateTVShow(TVShow movie);

        void DeleteMovie(long id);
        void DeleteMusic(long id);
        void DeleteBook(long id);
        void DeleteTVShow(long id);

    }
}
