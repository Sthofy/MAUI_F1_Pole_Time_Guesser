namespace PoleTimeGuesser.Api.Repositories.Contracts
{
    public interface IGuessRepository
    {
        Task<IEnumerable<GuessModel>> GuessGetByUserId(int userId);
        Task Insert(GuessRequest request);
        Task Update(GuessRequest request);
    }
}