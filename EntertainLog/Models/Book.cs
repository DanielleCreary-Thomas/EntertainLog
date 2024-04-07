using System.ComponentModel.DataAnnotations;

namespace EntertainLog.Models
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
    /// Represents the Book Entity which inherits from the Content class
    /// </summary>
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
