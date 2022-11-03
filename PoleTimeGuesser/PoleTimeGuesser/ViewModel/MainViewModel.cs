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
        IF1DataGetterService _f1DataGetterService;

        public MainViewModel(ISharedData sharedData,IF1DataGetterService f1DataGetterService)
        {
            _sharedData = sharedData;
            _f1DataGetterService = f1DataGetterService;
            Init = Initialize();
        }

        private async Task Initialize()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                UpcomingEvent = await _f1DataGetterService.GetUpComingEvent();
                var response = await _f1DataGetterService.GetTopDrivers();
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
