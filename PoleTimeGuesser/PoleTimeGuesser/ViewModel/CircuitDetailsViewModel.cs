// TODO: Dátumra kattinva => egész napos esemény a naptárba 

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

        IF1DataGetterService _f1DataGetterService;

        public CircuitDetailsViewModel(IF1DataGetterService f1DataGetterService)
        {
            _f1DataGetterService = f1DataGetterService;
        }

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
            CircuitInfo = await _f1DataGetterService.GetCicuitInfoAsync(Circuit.Circuit.CircuitId);
        }
    }
}
