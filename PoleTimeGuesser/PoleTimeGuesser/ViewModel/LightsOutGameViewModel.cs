namespace PoleTimeGuesser.ViewModel
{
    public partial class LightsOutGameViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _eTime = "Best";
        [ObservableProperty]
        private string _hText;
        [ObservableProperty]
        private string btnText = "Start";
        [ObservableProperty]
        private string _lightFirst;
        [ObservableProperty]
        private string _lightSecond;
        [ObservableProperty]
        private string _lightThird;
        [ObservableProperty]
        private bool _isClickable = true;
        private bool _isStarted = false;
        Stopwatch stopwatch = new Stopwatch();

        public LightsOutGameViewModel()
        {
            LightFirst = LightSecond = LightThird = "White";
            HText = "Can you faster then F1 Driver?\n Let's try!";
        }

        [RelayCommand]
        async void Game()
        {
            if (!_isStarted)
            {
                BtnText = "Stop";
                _isStarted = true;
                IsClickable = false;

                Random rnd = new Random();

                LightFirst = "Red";
                await Task.Delay(1000);
                LightSecond = "Red";
                await Task.Delay(1000);
                LightThird = "Red";
                await Task.Delay(rnd.Next(1000, 3000));
                LightFirst = LightSecond = LightThird = "White";

                IsClickable = true;

                stopwatch.Start();
            }
            else if (_isStarted)
            {
                BtnText = "Start";
                _isStarted = false;
                stopwatch.Stop();
                ETime = stopwatch.Elapsed.ToString(@"mm\:ss\:fff");
                stopwatch.Reset();
            }
        }
    }
}
