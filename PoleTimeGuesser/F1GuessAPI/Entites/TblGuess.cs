namespace F1GuessAPI.Entites
{
    public class TblGuess
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public TblUser User { get; set; } = new TblUser();
        public string Guess { get; set; } = "00:00:000";
        public int EventId { get; set; }
    }
}
