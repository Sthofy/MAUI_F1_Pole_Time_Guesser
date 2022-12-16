namespace PoleTimeGuesser.ViewModel
{
    public partial class MainViewModel : BaseViewModel
    {
        [ObservableProperty]
        ISharedData _sharedData;
        [ObservableProperty]
        ScheduleModel _upcomingEvent;
        [ObservableProperty]
        string _userPoints = "0";
        [ObservableProperty]
        string _ellapsedEvents = "22/22";
        public ObservableCollection<DriverStandingsModel> TopDrivers { get; } = new();
        public ObservableCollection<ScoreboardModel> Scoreboard { get; } = new();
        public ObservableCollection<ConstructorStandingsModel> Constructors { get; } = new();
        public Task Init { get; }
        IF1DataGetterService _f1DataGetterService;
        IServiceManager _serviceManager;
        public MainViewModel(ISharedData sharedData, IF1DataGetterService f1DataGetterService, IServiceManager serviceManager)
        {
            _sharedData = sharedData;
            _f1DataGetterService = f1DataGetterService;
            _serviceManager = serviceManager;
            Init = Initialize();
        }

        private async Task Initialize()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                await GetScoreboard();
                GetLoggedInUserPoints();
                await GetUpcomingEvent();

                var drivers = await _f1DataGetterService.GetTopDrivers();
                drivers.ForEach(x => TopDrivers.Add(x));

                var constructors = await _f1DataGetterService.GetConstructorStandings();
                constructors.ForEach(x => Constructors.Add(x));
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

        private async Task GetUpcomingEvent()
        {
            UpcomingEvent = await _f1DataGetterService.GetUpComingEvent();
            if (UpcomingEvent is null)
            {
                UpcomingEvent = new ScheduleModel
                {
                    Circuit = new CircuitModel
                    {
                        Location = new LocationModel
                        {
                            Image = "pin.jpg",
                            Country = "Season ended.",
                            Locality = "See you next year!"
                        },
                        CircuitName = ""
                    },
                    Date = "N.A.",
                };
            }
        }

        private async Task GetScoreboard()
        {
            var response = await _serviceManager.CallWebAPI<int?>("/User/GetScoreboard", HttpMethod.Get, null);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var listOfScoreboard = JsonConvert.DeserializeObject<IEnumerable<ScoreboardModel>>(responseContent).ToList();

                if (listOfScoreboard.Count == 0)
                    return;

                listOfScoreboard.ForEach(x => Scoreboard.Add(x));
            }

        }

        private void GetLoggedInUserPoints()
        {
            if (Scoreboard.Count == 0)
                return;

            UserPoints = Scoreboard.Where(x => x.Username == _sharedData.Username).Select(x => x.Score).First();
        }
        [RelayCommand]
        async Task GoToSettingsPage()
        {
            await Shell.Current.GoToAsync(nameof(SettingsView));
        }

        // TODO: Megcsinálni a csapat táblázatot
    }
}
