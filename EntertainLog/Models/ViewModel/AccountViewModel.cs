namespace EntertainLog.Models.ViewModel
{
    public class AccountViewModel
    {
        public IEnumerable<Book> BooksHist { get; set; }
        public IEnumerable<Movie> MoviesHist { get; set; }
        public IEnumerable<TVShow> TVShowsHist { get; set; }
        public IEnumerable<Music> MusicsHist { get; set; }
        public User CurrUser { get; set; }
    }
}
