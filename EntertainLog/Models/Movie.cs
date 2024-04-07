using System.ComponentModel.DataAnnotations;

namespace EntertainLog.Models
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
    /// Represents the Movie Entity which inherits from the Content class
    /// </summary>
    public class Movie : Content
    {
        public Movie() { }
        [Key]
        public long MovieID { get; set; }

        public String? Director { get; set; }

        public String Runtime { get; set; }

        public String Year { get; set; }

        public Boolean Watched { get; set; } = false;
    }
}
