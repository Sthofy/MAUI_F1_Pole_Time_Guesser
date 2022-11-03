namespace PoleTimeGuesser.Model
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Question { get; set; } = null!;
        public string AnswerA { get; set; } = null!;
        public string AnswerB { get; set; } = null!;
        public string AnswerC { get; set; } = null!;
    }
}
