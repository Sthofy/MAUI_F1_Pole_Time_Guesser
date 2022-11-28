namespace PoleTimeGuesser.Api.Repositories.Contracts
{
    public interface IScoreRepository
    {
        Task Insert(ScoreRequest request);
        Task Update(ScoreRequest request);
    }
}