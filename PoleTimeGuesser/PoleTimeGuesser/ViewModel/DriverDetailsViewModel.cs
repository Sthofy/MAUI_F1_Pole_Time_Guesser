namespace PoleTimeGuesser.ViewModel
{
    [QueryProperty("Driver", "Driver")]
    [QueryProperty("DriverInfo", "DriverInfoModel")]
    public partial class DriverDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        DriverModel driver;

        [ObservableProperty]
        DriverInfoModel driverInfo;
    }
}