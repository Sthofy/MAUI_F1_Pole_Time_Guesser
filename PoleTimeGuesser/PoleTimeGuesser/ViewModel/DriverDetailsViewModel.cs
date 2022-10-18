using PoleTimeGuesser.Model;
using PoleTimeGuesser.Services;

namespace PoleTimeGuesser.ViewModel
{
    [QueryProperty("Driver", "Driver")]
    public partial class DriverDetailsViewModel : BaseViewModel
    {
        public Task Init { get; }
        WebScrapper _scrapper = new WebScrapper();

        [ObservableProperty]
        DriverModel driver;

        [ObservableProperty]
        DriverInfoModel driverInfo;

        [ObservableProperty]
        bool isRefreshing;

        public DriverDetailsViewModel()
        {
            Init = Initailize();
        }

        private async Task Initailize()
        {
            await GetDriverInfoAsync();
        }

        public async Task GetDriverInfoAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                driverInfo = await _scrapper.GetDriverInfo(driver);

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
    }
}