namespace PoleTimeGuesser.ViewModel
{
    public partial class GuessViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _selectedDriverImage = "default_avatar.png";
        [ObservableProperty]
        private DriverStandingsModel _selectedDriver = null;
        [ObservableProperty]
        private string _selectedEventImage = "pin.png";
        [ObservableProperty]
        private ScheduleModel _selectedEvent = null;
        [ObservableProperty]
        int _minutes;
        [ObservableProperty]
        int _seconds;
        [ObservableProperty]
        int _miliseconds;
        private readonly string _gameRules;
        public ObservableCollection<DriverStandingsModel> Drivers { get; } = new();
        public ObservableCollection<ScheduleModel> Events { get; } = new();
        public ObservableCollection<GuessModel> PreviousGuesses { get; } = new();

        private readonly IF1DataGetterService _f1DataGetterService;
        private readonly ISharedData _sharedData;
        private readonly IServiceManager _serviceManager;
        private Task Init { get; }

        public GuessViewModel(IF1DataGetterService f1DataGetterService, ISharedData sharedData, IServiceManager serviceManager)
        {
            _gameRules = "Szia!" +
                "\n\nA Tipp játék során a feldatod egyszerű." +
                "\n\nTippeld meg ki nyeri az időmérőt és milyen idővel szerzi meg azt!";

            _f1DataGetterService = f1DataGetterService;
            _sharedData = sharedData;
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

                await GetDrivers();
                await GetEvents();
                await GetPreviousGuesses();
                await VerifyGuess();
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GetDrivers()
        {
            var driversStandigs = await _f1DataGetterService.GetDriverStandings();
            if (Drivers.Count != 0)
                Drivers.Clear();

            foreach (var item in driversStandigs)
            {
                Drivers.Add(item);
            }
        }
        private async Task GetEvents()
        {
            var schedules = await _f1DataGetterService.GetSchedule();
            if (Events.Count != 0)
                Events.Clear();

            foreach (var item in schedules)
            {
                //if (DateTime.Parse(item.Date).CompareTo(_sharedData.Date) == 1)
                //{
                Events.Add(item);
                //}
            }
        }
        private async Task GetPreviousGuesses()
        {
            var response = await _serviceManager.CallWebAPI<int>("/Game/GuessByUserId", HttpMethod.Post, _sharedData.Id);
            if(response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var guesses = JsonConvert.DeserializeObject<IEnumerable<GuessModel>>(responseContent).ToList();

                if (guesses is null)
                    return;

                if (PreviousGuesses.Count != 0)
                    PreviousGuesses.Clear();

                guesses.ForEach(x => PreviousGuesses.Add(x));
            }
        }

        private async Task VerifyGuess()
        {
            if (PreviousGuesses is null)
                return;

            foreach (var item in PreviousGuesses)
            {
                if (item.Difference.Equals("Soon"))
                {
                    var raceEvent = Events.Where(x => x.Circuit.CircuitId.ToUpper() == item.EventId).FirstOrDefault();
                    if (DateTime.Parse(raceEvent.Date) < DateTime.Now)
                    {
                        var result = await _f1DataGetterService.GetQualifyingResult(raceEvent.Round);
                        bool isCorrectDriver = item.DriverId.Equals(result.Driver.code);
                        double timeDiff = TimeSpan.Parse($"0:0{result.Q3}").TotalMilliseconds - TimeSpan.Parse($"0:0{item.Guess}").TotalMilliseconds;
                        int score = CalculateScore(isCorrectDriver, Convert.ToInt32(timeDiff));


                        await UpdateGuessDiff(_sharedData.Id, timeDiff.ToString(), item.EventId);

                        if (score > 0)
                            await UpdateScore(_sharedData.Id, score);
                    }

                }
            }

            await GetPreviousGuesses();
        }

        [RelayCommand]
        async Task ShowInfo()
        {
            await AppShell.Current.DisplayAlert("Info", _gameRules, "OK");
        }

        [RelayCommand]
        void GetSelectedDriver()
        {
            if (SelectedDriver != null)
            {
                var driver = Drivers.Where(x => x == SelectedDriver).FirstOrDefault();
                SelectedDriverImage = driver.Driver.Image.Front;
            }
        }

        [RelayCommand]
        void GetSelectedEvent()
        {
            if (SelectedEvent != null)
            {
                var sEvent = Events.Where(x => x == SelectedEvent).FirstOrDefault();
                SelectedEventImage = sEvent.Circuit.Location.Image;
            }
        }

        [RelayCommand]
        async void TakeGuess()
        {
            if (SelectedEvent is null || SelectedDriver is null)
            {
                await Shell.Current.DisplayAlert("Missing data", "Please select a driver or event!", "OK");
                return;
            }

            var request = new GuessRequest
            {
                UserId = _sharedData.Id,
                Guess = $"{Minutes}:{Seconds}.{Miliseconds}",
                EventId = SelectedEvent.Circuit.CircuitId.ToUpper(),
                DriverId = SelectedDriver.Driver.code.ToUpper(),
                Difference = "Soon"
            };
            var response = await _serviceManager.CallWebAPI<GuessRequest>("/Game/InsertGuess", HttpMethod.Post, request);

            ResetGuess();
            await GetPreviousGuesses();
        }

        [RelayCommand]
        void ResetGuess()
        {
            Minutes = 0;
            Seconds = 0;
            Miliseconds = 0;
            SelectedDriver = null;
            SelectedDriverImage = "default_avatar.png";
            SelectedEvent = null;
            SelectedEventImage = "pin.png";
        }

        private int CalculateScore(bool isCorrectDriver, int timeDiff)
        {
            int score = 0;

            if (isCorrectDriver)
                score += 1000;

            // TODO: Konstansba helyezni a pontozási érékeket és határokat 
            if (timeDiff >= -1000 && timeDiff <= 1000)
                score += (Math.Abs(timeDiff) * 10);

            return score;
        }

        private async Task UpdateScore(int userId, int score)
        {
            var request = new ScoreRequest
            {
                UserId = userId,
                Score = score
            };
            var result = await _serviceManager.CallWebAPI<ScoreRequest>("/Game/UpdateScore", HttpMethod.Put, request);
        }

        private async Task UpdateGuessDiff(int userId, string diff, string eventId)
        {
            var request = new GuessRequest
            {
                UserId = userId,
                Difference = diff,
                EventId = eventId,
                DriverId = "",
                Guess = ""
            };
            var result = await _serviceManager.CallWebAPI<GuessRequest>("/Game/UpdateGuessDiff", HttpMethod.Put, request);
        }
    }
}
