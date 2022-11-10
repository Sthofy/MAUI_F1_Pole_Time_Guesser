namespace F1GuessAPI.DataAccess.Game
{
    public class QuestionData : IQuestionData
    {
        ISqlDataAccess _sql;
        public QuestionData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public ListOfQuestions GetQuestion()
        {
            var data = _sql.LoadData<QuestionModel, dynamic>("dbo.spQuestion_GetAll", new { }, "F1GuessLocal");
            var output = new ListOfQuestions
            {
                Questions = data,
            };
            return output;
        }
    }
}
