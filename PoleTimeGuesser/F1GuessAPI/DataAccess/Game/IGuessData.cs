using F1GuessAPI.Controllers.Game;

namespace F1GuessAPI.DataAccess.Game
{
    public interface IGuessData
    {
        ListOfGuessModel GetByUserId(int userId);
        bool Insert(GuessRequest request);
        bool Update(GuessRequest request);
    }
}