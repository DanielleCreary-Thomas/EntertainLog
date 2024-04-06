namespace EntertainLog.Models.ViewModel
{
    public class MovieViewModel
    {
        public IEnumerable<Movie> Movies { get; set; }

        public Movie NewMovie { get; set; }
        public User CurrUser { get; set; }

        public Movie CurrMovie { get; set; }
    }
}
