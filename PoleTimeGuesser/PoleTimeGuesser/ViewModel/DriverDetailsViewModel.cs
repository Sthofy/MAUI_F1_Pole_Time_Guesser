namespace PoleTimeGuesser.ViewModel
{
    [QueryProperty("Driver", "Driver")]
    public partial class DriverDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        DriverModel driver;
    }
}