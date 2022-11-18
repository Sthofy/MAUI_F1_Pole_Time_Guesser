using F1GuessAPI.Controllers.Game;

namespace F1GuessAPI.DataAccess.Game
{
    public class GuessData : IGuessData
    {
        ISqlDataAccess _sql;

        public GuessData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public bool Insert(GuessRequest request)
        {
            try
            {
                //TODO : ConnectionStringet kiemelni globális elérésbe

                _sql.SaveData("spGuess_Insert", request, "F1GuessLocal");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(GuessRequest request)
        {
            var response = _sql.LoadData<GuessModel, dynamic>("dbo.spGuess_GetByUserId", new { UserId = userId }, "F1GuessLocal").FirstOrDefault();
            // TODO: Befejezni a logikát
        }

        public ListOfGuessModel GetByUserId(int userId)
        {
            var response = _sql.LoadData<GuessModel, dynamic>("dbo.spGuess_GetByUserId", new { UserId = userId }, "F1GuessLocal");

            var output = new ListOfGuessModel
            {
                Guesses = response
            };
            return output;
        }
    }
}
