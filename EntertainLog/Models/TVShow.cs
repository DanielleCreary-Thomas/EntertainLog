using System.ComponentModel.DataAnnotations;

namespace EntertainLog.Models
{
    public class TVShow : Content
    {
        /**
         * TVShowID:Int
            Creator:String
            Seasons:Int
            Episodes:Int
            consumed:->Watched:Bool

         */
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
