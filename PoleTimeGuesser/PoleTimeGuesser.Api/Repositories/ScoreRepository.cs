using Azure.Core;
using PoleTimeGuesser.Api.DataAccess;
using PoleTimeGuesser.Api.Repositories.Contracts;
using PoleTimeGuesser.Library.Models;

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

        public async Task Insert(int id, int score)
        {
            await _sql.SaveData("dbo.spUsersScoreboard_Insert", new { UserId = id, Score = score }, cnnString);
        }

        public async Task Update(int id, int score)
        {
            await _sql.SaveData("dbo.spUsersScoreboard_Update", new { UserId = id, Score = score }, cnnString);
        }
    }
}
