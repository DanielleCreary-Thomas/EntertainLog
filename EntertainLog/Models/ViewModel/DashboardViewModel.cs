namespace EntertainLog.Models.ViewModel
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
    /// The structure holding the objects required by the Dashboard related Views
    /// </summary>
    public class DashboardViewModel
    {
        public IEnumerable<Book> BooksQueue { get; set; }
        public IEnumerable<Movie> MoviesQueue { get; set; }
        public IEnumerable<TVShow> TVShowsQueue { get; set; }
        public IEnumerable<Music> MusicsQueue { get; set; }

        public IEnumerable<Book> BooksFaves { get; set; }
        public IEnumerable<Movie> MoviesFaves { get; set; }
        public IEnumerable<TVShow> TVShowsFaves { get; set; }
        public IEnumerable<Music> MusicsFaves { get; set; }

        public User CurrUser { get; set; }
        public long CurrUserId { get; set; }

    }
}
