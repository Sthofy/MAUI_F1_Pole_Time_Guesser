// TODO: Dátumra kattinva => egész napos esemény a naptárba 

using System.IO;

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
            var result = await _f1DataGetterService.GetCicuitInfoAsync(Circuit.Circuit.CircuitId);

            if (result is null)
            {
                // TODO: Error képernyő/not found képernyő
            }
            else
            {
                CircuitInfo = result;
            }
        }
    }
}
