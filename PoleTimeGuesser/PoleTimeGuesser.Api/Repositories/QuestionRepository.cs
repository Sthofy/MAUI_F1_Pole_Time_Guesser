namespace PoleTimeGuesser.Api.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly string cnnString = "F1GuessDB";
        private readonly ISqlDataAccess _sql;

        public QuestionRepository(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public async Task<IEnumerable<QuestionModel>> GetQuestions()
        {
            var response = await _sql.LoadData<QuestionModel, dynamic>("dbo.spQuestion_GetAll", new { }, cnnString);

            return response;
        }
    }
}
