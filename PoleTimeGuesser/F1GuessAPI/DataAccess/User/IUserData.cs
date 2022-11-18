namespace F1GuessAPI.DataAccess.User
{
    public interface IUserData
    {
        LoggedInUserModel? Authenticate(string username, string password);
        LoggedInUserModel? GetUserByUsername(string username);
        bool Registration(string username, string email, string password);
        bool Update(int id, string username, string email, string password);
    }
}