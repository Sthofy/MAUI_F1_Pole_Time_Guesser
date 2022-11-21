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

                _sql.SaveData("dbo.spGuess_Insert", request, "F1GuessLocal");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(GuessRequest request)
        {
            var data = _sql.LoadData<GuessModel, int>("dbo.spGuess_GetByUserId", request.UserId, "F1GuessLocal").FirstOrDefault();

            if (!request.Guess.Equals(data.Guess))
                data.Guess = request.Guess;
            if (!request.Difference.Equals(data.Difference))
                data.Difference = request.Difference;
            if (request.DriverId.Equals(data.DriverId))
                data.DriverId = request.DriverId;

            _sql.SaveData("dbo.spGuess_Update", new { UserId = data.Id, EventId = data.EventId, Difference = data.Difference }, "F1GuessLocal");

            return true;
        }

        public bool UpdateDiff(GuessRequest request)
        {
            try
            {
                _sql.SaveData("dbo.spGuess_UpdateDiff", new { UserId = request.UserId, EventId = request.EventId, Difference = request.Difference }, "F1GuessLocal");

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
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
