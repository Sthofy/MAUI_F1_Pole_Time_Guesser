namespace PoleTimeGuesser.Api.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<LoggedInUserModel> Authenticate(string username, string password);
        Task<bool> GetByUsername(string username);
        Task<IEnumerable<ScoreboardModel>> GetScoreboard();
        Task<RegistrationModel> Registration(string username, string email, string password);
        Task<bool> Update(int id, string username, string email, string password);
    }
}
