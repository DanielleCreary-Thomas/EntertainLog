using System.ComponentModel.DataAnnotations;

namespace EntertainLog.Models
{
    public class User
    {
        public User() { }
        [Key]
        public long UserID { get; set; }
    }
}
