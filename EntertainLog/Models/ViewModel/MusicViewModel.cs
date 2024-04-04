namespace EntertainLog.Models.ViewModel
{
    public class MusicViewModel
    {
        public IEnumerable<Music> Musics { get; set; }

        public Music NewMusic { get; set; }
        public User CurrUser { get; set; }

        public Music CurrMusic { get; set; }
    }
}
