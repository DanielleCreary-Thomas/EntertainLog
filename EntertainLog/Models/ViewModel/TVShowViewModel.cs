namespace EntertainLog.Models.ViewModel
{
    public class TVShowViewModel
    {
        public IEnumerable<TVShow> TVShows { get; set; }

        public TVShow NewTVShow { get; set; }
        public User CurrUser { get; set; }

        public TVShow CurrTVShow { get; set; }
    }
}
