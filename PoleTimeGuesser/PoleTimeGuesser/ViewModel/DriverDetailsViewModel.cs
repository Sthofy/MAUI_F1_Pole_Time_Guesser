namespace PoleTimeGuesser.ViewModel
{
    [QueryProperty("Driver", "Driver")]
    public partial class DriverDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        DriverModel driver;

        [ObservableProperty]
        DriverInfoModel driverInfo;

        F1DataGetterService f1DataGetterService = new F1DataGetterService();

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
            DriverInfo = await f1DataGetterService.GetDriverInfo(Driver.driverId);
        }
    }
}