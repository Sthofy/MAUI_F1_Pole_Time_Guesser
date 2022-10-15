using PoleTimeGuesser.Model;
using PoleTimeGuesser.Services;

namespace PoleTimeGuesser.ViewModel
{
    public partial class DriverStandingsViewModel : BaseViewModel
    {
        IF1DataGetterService _f1DataGetterService;
        public ObservableCollection<DriverStandingsModel> driverStandigsModels { get; } = new();

        public DriverStandingsViewModel(IF1DataGetterService f1DataGetterService)
        {
            Title = "Driver Standings";
            _f1DataGetterService = f1DataGetterService;
        }

        [RelayCommand]
        async Task GetDriverStandigsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var driversStandigs = await _f1DataGetterService.GetDriverStandings();
                if (driverStandigsModels.Count != 0)
                    driverStandigsModels.Clear();

                foreach (var item in driversStandigs)
                {
                    Console.WriteLine(item);
                    driverStandigsModels.Add(item);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
