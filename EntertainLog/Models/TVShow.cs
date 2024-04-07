using System.ComponentModel.DataAnnotations;

namespace EntertainLog.Models
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
    /// Represents the TVShow Entity which inherits from the Content class
    /// </summary>
    public class TVShow : Content
    {
        public TVShow() { }

        [Key]
        public long TVShowID { get; set; }

        [Required(ErrorMessage = "Please enter the Creator")]
        public String? Creator {  get; set; }

        public int Seasons { get; set; } = 1;
        //public int Episodes

        public Boolean Watched { get; set; } = false;

    }
}
