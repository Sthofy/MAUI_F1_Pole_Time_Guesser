namespace PoleTimeGuesser.ViewModel
{
    public partial class GuessViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _selectedDriverImage = "default_avatar.png";
        [ObservableProperty]
        private DriverStandingsModel _selectedDriver;
        private readonly string _gameRules;
        public ObservableCollection<DriverStandingsModel> Drivers { get; } = new();
        private readonly IF1DataGetterService _f1DataGetterService;
        private Task Init { get; }

        public GuessViewModel(IF1DataGetterService f1DataGetterService)
        {
            _gameRules = "Szia!" +
                "\n\nA Tipp játék során a feldatod egyszerű." +
                "\n\nTippeld meg ki nyeri az időmérőt és milyen idővel szerzi meg azt!";
            _f1DataGetterService = f1DataGetterService;
            Init = Initialize();
        }

        private async Task Initialize()
        {
            await GetDrivers();
        }

        private async Task GetDrivers()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var driversStandigs = await _f1DataGetterService.GetDriverStandings();
                if (Drivers.Count != 0)
                    Drivers.Clear();

                foreach (var item in driversStandigs)
                {
                    Drivers.Add(item);
                }
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
        async Task ShowInfo()
        {
            await AppShell.Current.DisplayAlert("Info", _gameRules, "OK");
        }

        [RelayCommand]
        async Task GetSelectedDriver()
        {
            var driver = Drivers.Where(x => x == SelectedDriver).FirstOrDefault();
            SelectedDriverImage = driver.Driver.Image.Front;
        }
    }
}
