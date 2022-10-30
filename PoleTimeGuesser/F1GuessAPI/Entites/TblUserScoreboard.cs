namespace F1GuessAPI.Entites
{
    public class TblUserScoreboard
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public TblUser User { get; set; } = new TblUser();
        public int Score { get; set; }
    }
}
