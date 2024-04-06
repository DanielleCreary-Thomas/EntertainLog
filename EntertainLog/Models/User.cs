using System.ComponentModel.DataAnnotations;

namespace EntertainLog.Models
{
    public class User
    {
        public User() { }
        [Key]
        public long UserID { get; set; }

        [Required(ErrorMessage = "Please enter the Username")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Please enter the Email")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Please enter the Password")]
        public String Password { get; set; }
        


    }
}
