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

                _sql.SaveData("spUsersScoreboard_Insert", data, "F1GuessLocal");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
