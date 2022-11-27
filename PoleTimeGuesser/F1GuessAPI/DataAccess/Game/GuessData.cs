using F1GuessAPI.Controllers.Game;

namespace F1GuessAPI.DataAccess.Game
{
    public class GuessData : IGuessData
    {
        ISqlDataAccess _sql;
        private readonly string cnnStringLocal = "F1GuessLocal";
        private readonly string cnnString = "F1GuessDB";

        public GuessData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public bool Insert(GuessRequest request)
        {
            try
            {
                //TODO : ConnectionStringet kiemelni globális elérésbe

                _sql.SaveData("dbo.spGuess_Insert", request, cnnString);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(GuessRequest request)
        {
            var data = _sql.LoadData<GuessModel, int>("dbo.spGuess_GetByUserId", request.UserId, cnnString).FirstOrDefault();

            if (!request.Guess.Equals(data.Guess))
                data.Guess = request.Guess;
            if (!request.Difference.Equals(data.Difference))
                data.Difference = request.Difference;
            if (request.DriverId.Equals(data.DriverId))
                data.DriverId = request.DriverId;

            _sql.SaveData("dbo.spGuess_Update", new { UserId = data.Id, EventId = data.EventId, Difference = data.Difference }, cnnString);

            return true;
        }

        public bool UpdateDiff(GuessRequest request)
        {
            try
            {
                _sql.SaveData("dbo.spGuess_UpdateDiff", new { UserId = request.UserId, EventId = request.EventId, Difference = request.Difference }, cnnString);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public ListOfGuessModel GetByUserId(int userId)
        {
            var response = _sql.LoadData<GuessModel, dynamic>("dbo.spGuess_GetByUserId", new { UserId = userId }, cnnString);

            var output = new ListOfGuessModel
            {
                Guesses = response
            };
            return output;
        }
    }
}
