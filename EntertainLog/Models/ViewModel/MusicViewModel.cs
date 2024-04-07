namespace EntertainLog.Models.ViewModel
{
    /// <summary>
    /// Created By: Danielle Creary-Thomas
    /// The structure holding the objects required by the Music related Views
    /// </summary>
    public class MusicViewModel
    {
        public IEnumerable<Music> Musics { get; set; }

        public Music NewMusic { get; set; }
        public User CurrUser { get; set; }

        public Music CurrMusic { get; set; }
    }
}
