namespace F1GuessAPI.Controllers.Game
{
    public class GuessRequest
    {
        public int UserId { get; set; }
        public string Guess { get; set; } = null!;
        public string EventId { get; set; } = null!;
        public string DriverId { get; set; } = null!;
    }
}
