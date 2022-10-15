using PoleTimeGuesser.Model;

namespace PoleTimeGuesser.Services
{
    public interface IF1DataGetterService
    {
        Task<List<DriverStandingsModel>> GetDriverStandings();
    }
}