using System.ComponentModel.DataAnnotations;

namespace EntertainLog.Models
{
    public class Book : Content
    {
        public Book() { }

        [Key]
        public long BookID { get; set; }

        [Required(ErrorMessage = "Please enter the Author")]

        public String? Author { get; set; }
        public long? PageCount { get; set; }

        public Boolean Read { get; set; } = false;
    }
}
