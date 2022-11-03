namespace PoleTimeGuesser.ViewModel
{
    [QueryProperty("Driver", "Driver")]
    public partial class DriverDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        DriverModel driver;

        [ObservableProperty]
        DriverInfoModel driverInfo;

        IF1DataGetterService _f1DataGetterService;

        public DriverDetailsViewModel(IF1DataGetterService f1DataGetterService)
        {
            _f1DataGetterService = f1DataGetterService;
        }

        public void Initailize()
        {
            Task.Run(async () =>
            {
                IsBusy = true;
                await GetDriverInfo();
            }).GetAwaiter().OnCompleted(() =>
            {
                IsBusy = false;
            });
        }

        [RelayCommand]
        private async Task GetDriverInfo()
        {
            DriverInfo = await _f1DataGetterService.GetDriverInfo(Driver.driverId);
        }
    }
}