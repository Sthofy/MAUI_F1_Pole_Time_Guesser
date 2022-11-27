using F1GuessAPI.Controllers.User;

namespace F1GuessAPI.DataAccess.User
{
    public interface IUserData
    {
        LoggedInUserModel? Authenticate(string username, string password);
        ScoreboardResponse GetScoreboard();
        bool GetUserByUsername(string username);
        RegistrationModel Registration(string username, string email, string password);
        bool Update(int id, string username, string email, string password);
    }
}