namespace PoleTimeGuesser.Library.Models
{
    public class ScheduleModel
    {
        public string Round { get; set; }
        public string RaceName { get; set; }
        public CircuitModel Circuit { get; set; }
        public string Date { get; set; }
    }
}
