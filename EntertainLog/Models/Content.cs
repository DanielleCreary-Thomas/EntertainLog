using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntertainLog.Models
{
    public class Content
    {
        public Content() { }

        [Required(ErrorMessage = "Please enter the Title")]
        public String? Title { get; set; }

        [Required(ErrorMessage = "Please enter the Year")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Please enter the Genre")]
        public String? Genre { get; set; }

        public Boolean IsConsumed { get; set; } = false;

        public int? Rating { get; set; }
        public String? Notes { get; set; }

        [ForeignKey("UserID")]
        public long UserID { get; set; }


    }
}
