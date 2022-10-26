﻿namespace PoleTimeGuesser.ViewModel
{
    public partial class DriverStandingsViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;

        public Task Init { get; }
        public ObservableCollection<DriverStandingsModel> driverStandingsModels { get; } = new();
        F1DataGetterService f1DataGetterService = new F1DataGetterService();
        WebScrapper _scrapper = new WebScrapper();

        public DriverStandingsViewModel()
        {
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
                var driversStandigs = await f1DataGetterService.GetDriverStandings();
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

            DriverInfoModel driverinfo = new DriverInfoModel();
            driverinfo = await _scrapper.GetDriverInfo(driverstandings.Driver.driverId);
            //var driverinfo = await f1DataGetterService.GetDriverInfo(driverstandings.Driver.driverId);

            await Shell.Current.GoToAsync($"{nameof(DriverDetailsView)}", true,
                new Dictionary<string, object>
                {
                    { "Driver" , driverstandings.Driver },
                    { "DriverInfoModel" , driverinfo }
                });
        }
    }
}
