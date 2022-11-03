namespace F1GuessAPI.Functions.Game
{
    public interface IQuestionFunctions
    {
        Task<List<QuestionModel>> GetQuestion();
    }
}