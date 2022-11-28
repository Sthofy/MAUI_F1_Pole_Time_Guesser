namespace PoleTimeGuesser.Api.Repositories.Contracts
{
    public interface IScoreRepository
    {
        Task Insert(int id, int score);
        Task Update(int id, int score);
    }
}