namespace F1GuessAPI.DataAccess.Game
{
    public class ScoreData : IScoreData
    {
        ISqlDataAccess _sql;

        public ScoreData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public bool Insert(int id, int score)
        {

            try
            {
                //TODO : ConnectionStringet kiemelni globális elérésbe
                var data = new UsersScoreboardModel
                {
                    UserId = id,
                    Score = score
                };

                _sql.SaveData("dbo.spUsersScoreboard_Insert", data, "F1GuessLocal");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(ScoreRequest request)
        {
            try
            {
                var data = _sql.LoadData<UsersScoreboardModel, dynamic>("dbo.spUsersScoreboard_GetByUserId", new { UserId = request.UserId }, "F1GuessLocal").FirstOrDefault();

                int score = data.Score + request.Score;
                _sql.SaveData("dbo.spUsersScoreboard_Update", new { UserId = request.UserId, Score = score }, "F1GuessLocal");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
