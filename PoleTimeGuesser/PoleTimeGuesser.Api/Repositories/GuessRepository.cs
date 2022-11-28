namespace PoleTimeGuesser.Api.Repositories
{
    public class GuessRepository : IGuessRepository
    {
        private readonly ISqlDataAccess _sql;
        private readonly string cnnString = "F1GuessDB";

        public GuessRepository(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public async Task Insert(GuessRequest request)
        {
            await _sql.SaveData("dbo.spGuess_Insert", request, cnnString);
        }

        public async Task Update(GuessRequest request)
        {
            // TODO: Frissíteni a Drivert, időt ha az más

            await _sql.SaveData("dbo.spGuess_Update",
                new { UserId = request.UserId, EventId = request.EventId, Difference = request.Difference },
                cnnString);
        }

        public async Task<IEnumerable<GuessModel>> GuessGetByUserId(int userId)
        {
            var response = await _sql.LoadData<GuessModel, dynamic>("dbo.spGuess_GetByUserId", new { UserId = userId }, cnnString);

            return response;
        }
    }
}
