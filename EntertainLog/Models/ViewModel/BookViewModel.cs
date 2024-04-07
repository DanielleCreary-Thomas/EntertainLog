namespace EntertainLog.Models.ViewModel
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
    /// The structure holding the objects required by the Book related Views
    /// </summary>
    public class BookViewModel
    {
        public IEnumerable<Book> Books { get; set; }

        public Book NewBook { get; set; }
        public User CurrUser { get; set; }

        public Book CurrBook { get; set; }
    }
}
