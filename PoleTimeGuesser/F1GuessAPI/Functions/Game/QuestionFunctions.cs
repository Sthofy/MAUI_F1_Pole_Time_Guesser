namespace F1GuessAPI.Functions.Game
{
    public class QuestionFunctions : IQuestionFunctions
    {
        F1GuessContext _context;
        public QuestionFunctions(F1GuessContext context)
        {
            _context = context;
        }

        public async Task<List<QuestionModel>> GetQuestion()
        {
            var data = await _context.TblQuestions.ToListAsync();
            List<QuestionModel> response = new List<QuestionModel>();

            foreach (var item in data)
            {
                response.Add(new QuestionModel
                {
                    Id = item.Id,
                    Question = item.Question,
                    AnswerA = item.AnswerA,
                    AnswerB = item.AnswerB,
                    AnswerC = item.AnswerC
                });
            }

            return response;
        }
    }
}
