// TODO: Dátumra kattinva => egész napos esemény a naptárba 

namespace PoleTimeGuesser.ViewModel
{
    [QueryProperty("Circuit", "Circuit")]
    public partial class CircuitDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool _isRefreshing;

        [ObservableProperty]
        ScheduleModel _circuit;

        [ObservableProperty]
        CircuitInfoModel _circuitInfo;

        readonly IF1DataGetterService _f1DataGetterService;

        public CircuitDetailsViewModel(IF1DataGetterService f1DataGetterService)
        {
            _f1DataGetterService = f1DataGetterService;
            IsBusy = true;
            PageState = pStates.Loading.ToString();
        }

        public void Initailize()
        {
            try
            {
                Task.Run(async () =>
                {
                    await GetCircuitInfo();
                }).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                PageState = pStates.Error.ToString();
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GetCircuitInfo()
        {

            var result = await _f1DataGetterService.GetCicuitInfoAsync(Circuit.Circuit.CircuitId);

            if (result is null)
            {
                // TODO: Error képernyő/not found képernyő
                PageState = pStates.Error.ToString();
            }
            else
            {
                CircuitInfo = result;
                PageState = pStates.Success.ToString();
            }
        }
    }
}
