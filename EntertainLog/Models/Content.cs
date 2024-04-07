using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntertainLog.Models
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
    /// Represents the Content class, a superclass for all content subtypes
    /// </summary>
    public class Content
    {
        public Content() { }

        [Required(ErrorMessage = "Please enter the Title")]
        public String? Title { get; set; }

        [Required(ErrorMessage = "Please enter the Year")]
        public String? Year { get; set; }

        [Required(ErrorMessage = "Please enter the Genre")]
        public String? Genre { get; set; }

        public int? Rating { get; set; }

        public Boolean Favourited { get; set; } = false;

        public Boolean Queued { get; set; } = false;

        public String? Notes { get; set; }

        [ForeignKey("UserID")]
        public long UserID { get; set; }


    }
}
