namespace PoleTimeGuesser.Library.Requests
{
    public class GuessRequest
    {
        public int UserId { get; set; }
        public string Guess { get; set; }
        public string EventId { get; set; }
        public string DriverId { get; set; }
        public string Difference { get; set; }
    }
}
