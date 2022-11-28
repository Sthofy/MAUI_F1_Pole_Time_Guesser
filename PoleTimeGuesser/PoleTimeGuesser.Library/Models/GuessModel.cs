namespace PoleTimeGuesser.Library.Models
{
    public class GuessModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Guess { get; set; }
        public string EventId { get; set; }
        public string DriverId { get; set; }
        public string Difference { get; set; }
    }
}
