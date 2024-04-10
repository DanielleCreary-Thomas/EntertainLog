using System.ComponentModel.DataAnnotations;

namespace EntertainLog.Models.ViewModel
{
    /// <summary>
    /// Created By: Yuanlong Song
    /// The structure holding the objects required by the Login info
    /// </summary>
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }



}
