namespace PoleTimeGuesser.Library.Models
{
    public class DriverStandingsModel
    {
        public string position { get; set; }
        public string positionText { get; set; }
        public string points { get; set; }
        public string wins { get; set; }
        public DriverModel Driver { get; set; }
        public List<ConstructorsModel> Constructors { get; set; }
    }
}
