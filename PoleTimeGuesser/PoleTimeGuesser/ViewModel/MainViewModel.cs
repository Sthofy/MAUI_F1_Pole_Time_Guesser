namespace PoleTimeGuesser.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        [ObservableProperty]
        ISharedData _sharedData;
        [ObservableProperty]
        ScheduleModel _upcomingEvent;
        public ObservableCollection<DriverStandingsModel> TopDrivers { get; } = new();

        public Task Init { get; }

        F1DataGetterService f1DataGetterService = new F1DataGetterService();

        public MainViewModel(ISharedData sharedData)
        {
            _sharedData = sharedData;
            Init = Initialize();
        }

        private async Task Initialize()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                UpcomingEvent = await f1DataGetterService.GetUpComingEvent();
                var response = await f1DataGetterService.GetTopDrivers();
                response.ForEach(x => TopDrivers.Add(x));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToSettingsPage()
        {
            await Shell.Current.GoToAsync(nameof(SettingsView));
        }
    }
}
