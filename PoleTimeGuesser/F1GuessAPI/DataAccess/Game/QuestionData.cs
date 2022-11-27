namespace F1GuessAPI.DataAccess.Game
{
    public class QuestionData : IQuestionData
    {
        private readonly string cnnStringLocal = "F1GuessLocal";
        private readonly string cnnString = "F1GuessDB";
        ISqlDataAccess _sql;
        public QuestionData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public ListOfQuestions GetQuestion()
        {
            var data = _sql.LoadData<QuestionModel, dynamic>("dbo.spQuestion_GetAll", new { }, cnnString);
            var output = new ListOfQuestions
            {
                Questions = data,
            };
            return output;
        }
    }
}
