namespace EntertainLog.Models.ViewModel
{
    public class AccountViewModel
    {
        public IEnumerable<Book> BooksFaves { get; set; }
        public IEnumerable<Movie> MoviesFaves { get; set; }
        public IEnumerable<TVShow> TVShowsFaves { get; set; }
        public IEnumerable<Music> MusicsFaves { get; set; }
        public User CurrUser { get; set; }
    }
}
