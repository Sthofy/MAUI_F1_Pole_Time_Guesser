namespace F1GuessAPI.DataAccess.Game
{
    public interface IScoreData
    {
        bool Insert(int id, int score);
        bool Update(ScoreRequest request);
    }
}