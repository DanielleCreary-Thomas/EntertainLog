namespace EntertainLog.Models.ViewModel
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
    /// The structure holding the objects required by the TVShow related Views
    /// </summary>
    public class TVShowViewModel
    {
        public IEnumerable<TVShow> TVShows { get; set; }

        public TVShow NewTVShow { get; set; }
        public User CurrUser { get; set; }

        public TVShow CurrTVShow { get; set; }
    }
}
