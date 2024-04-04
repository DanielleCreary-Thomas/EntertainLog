using System.ComponentModel.DataAnnotations;

namespace EntertainLog.Models
{
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
