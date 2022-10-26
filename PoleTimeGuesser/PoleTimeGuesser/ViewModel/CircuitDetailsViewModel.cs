namespace PoleTimeGuesser.ViewModel
{
    [QueryProperty("Circuit", "Circuit")]
    public partial class CircuitDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        ScheduleModel circuit;

        [ObservableProperty]
        CircuitInfoModel circuitInfo;

        WebScrapper _scrapper = new WebScrapper();

        public void Initailize()
        {
            Task.Run(async () =>
            {
                IsBusy = true;
                await GetCircuitInfo();
            }).GetAwaiter().OnCompleted(() =>
            {
                IsBusy = false;
            });
        }

        [RelayCommand]
        private async Task GetCircuitInfo()
        {
            CircuitInfo = await _scrapper.GetCircuitInfoAsync(Circuit.Circuit.CircuitId);
        }
    }
}
