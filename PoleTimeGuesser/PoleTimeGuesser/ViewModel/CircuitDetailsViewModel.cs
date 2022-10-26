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

        F1DataGetterService f1DataGetterService = new F1DataGetterService();

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
            CircuitInfo = await f1DataGetterService.GetCicuitInfoAsync(Circuit.Circuit.CircuitId);
        }
    }
}
