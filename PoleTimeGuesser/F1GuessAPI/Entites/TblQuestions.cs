namespace F1GuessAPI.Entites
{
    public class TblQuestions
    {
        public int Id { get; set; }
        public string Question { get; set; } = null!;
        public string AnswerA { get; set; } = null!;
        public string AnswerB { get; set; } = null!;
        public string AnswerC { get; set; } = null!;
    }
}
