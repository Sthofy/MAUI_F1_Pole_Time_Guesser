namespace PoleTimeGuesser.Api.Repositories
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly ISqlDataAccess _sql;
        private readonly string cnnString = "F1GuessDB";

        public ScoreRepository(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public async Task Insert(ScoreRequest request)
        {
            await _sql.SaveData("dbo.spUsersScoreboard_Insert", request, cnnString);
        }

        public async Task Update(ScoreRequest request)
        {
            await _sql.SaveData("dbo.spUsersScoreboard_Update", request, cnnString);
        }
    }
}
