﻿namespace EntertainLog.Models.Repos
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
        Task<Music?> GetMusicByIDAsync(long id);
        Task<Book?> GetBookByIDAsync(long id);
        Task<TVShow?> GetTVShowByIDAsync(long id);
        Task<User?> GetUserByIDAsync(long id);


        Movie UpdateMovie(Movie movie);
        Music UpdateMusic(Music music);
        Book UpdateBook(Book book);
        TVShow UpdateTVShow(TVShow movie);

        void DeleteMovie(Movie movie);
        void DeleteMusic(Music music);
        void DeleteBook(Book book);
        void DeleteTVShow(TVShow tvshow);

    }
}
