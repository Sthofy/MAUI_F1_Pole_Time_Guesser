namespace PoleTimeGuesser.ViewModel
{
    [QueryProperty("Driver", "Driver")]
    public partial class DriverDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        DriverModel _driver;

        [ObservableProperty]
        DriverInfoModel _driverInfo;

        readonly IF1DataGetterService _f1DataGetterService;

        public DriverDetailsViewModel(IF1DataGetterService f1DataGetterService)
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
                    await GetDriverInfo();
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
        private async Task GetDriverInfo()
        {
            try
            {
                var result = await _f1DataGetterService.GetDriverInfo(Driver.driverId);
                if (result is null)
                {
                    // TODO: Error képernyő/not found képernyő
                    PageState = pStates.Error.ToString();
                }
                else
                {
                    DriverInfo = result;
                    PageState = pStates.Success.ToString();
                }
            }
            catch (Exception ex)
            {
                PageState = pStates.Error.ToString();
            }
        }
    }
}