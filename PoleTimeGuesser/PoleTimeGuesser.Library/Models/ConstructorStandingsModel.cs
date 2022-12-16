namespace PoleTimeGuesser.Library.Models
{
    public class ConstructorStandingsModel
    {
        public string Position { get; set; }
        public string Points { get; set; }
        public string Wins { get; set; }
        public ConstructorModel Constructor { get; set; }
    }
}
