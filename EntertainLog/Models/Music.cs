using System.ComponentModel.DataAnnotations;

namespace EntertainLog.Models
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
    /// Represents the Music Entity which inherits from the Content class
    /// </summary>
    public class Music : Content
    { 
        public Music() { }
        [Key]
        public long MusicID { get; set; }

        public String Artist { get; set; }

        public String Runtime { get; set; }

        public String Album { get; set; }

        public Boolean Listened { get; set; } = false;

    }
}
