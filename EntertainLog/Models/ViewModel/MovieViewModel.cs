namespace EntertainLog.Models.ViewModel
{
    /// <summary>
    /// Created By: Yuanlong
    /// The structure holding the objects required by the Movie related Views
    /// </summary>
    public class MovieViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }

        public Movie NewMovie { get; set; }
        public User CurrUser { get; set; }

        public Movie CurrMovie { get; set; }
    }
}
