namespace PoleTimeGuesser.ViewModel
{
    public partial class DriverStandingsViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;

        public Task Init { get; }
        public ObservableCollection<DriverStandingsModel> driverStandingsModels { get; } = new();
        IF1DataGetterService _f1DataGetterService;

        public DriverStandingsViewModel(IF1DataGetterService f1DataGetterService)
        {
            _f1DataGetterService = f1DataGetterService;
            Title = "Driver Standings";
            Init = Initialize();
        }

        private async Task Initialize()
        {
            await GetDriverStandigsAsync();
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
                if (driverStandingsModels.Count != 0)
                    driverStandingsModels.Clear();

                foreach (var item in driversStandigs)
                {
                    driverStandingsModels.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GoToDriverDetailsAsync(DriverStandingsModel driverstandings)
        {
            if (driverstandings is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(DriverDetailsView)}", true,
                new Dictionary<string, object>
                {
                    { "Driver" , driverstandings.Driver }
                });
        }
    }
}
