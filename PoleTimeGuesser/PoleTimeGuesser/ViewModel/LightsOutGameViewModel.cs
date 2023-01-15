using Microsoft.Maui.Graphics.Text;
using System.Globalization;

namespace PoleTimeGuesser.ViewModel
{
    public partial class LightsOutGameViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _eTime = "Best";
        [ObservableProperty]
        private string _pTime = "Best";
        [ObservableProperty]
        private string _hText;
        [ObservableProperty]
        private string _btnText = "Start";
        [ObservableProperty]
        private string _lightFirst;
        [ObservableProperty]
        private string _lightSecond;
        [ObservableProperty]
        private string _lightThird;
        [ObservableProperty]
        private bool _isClickable = true;
        private bool _isStarted = false;
        public ObservableCollection<string> PreviousTimes { get; } = new();
        readonly Stopwatch _stopwatch = new Stopwatch();

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

                _stopwatch.Start();
            }
            else if (_isStarted)
            {
                BtnText = "Start";
                _isStarted = false;
                _stopwatch.Stop();

                if (!ETime.Equals("Best"))
                {
                    var time = TimeSpan.ParseExact(ETime, "mm\\:ss\\:fff", CultureInfo.CurrentCulture).TotalMilliseconds;

                    if (time > _stopwatch.ElapsedMilliseconds)
                        ETime = _stopwatch.Elapsed.ToString(@"mm\:ss\:fff");
                }
                else
                {
                    ETime = _stopwatch.Elapsed.ToString(@"mm\:ss\:fff");
                }

                PTime = _stopwatch.Elapsed.ToString(@"mm\:ss\:fff");
                PreviousTimes.Add(PTime);
                _stopwatch.Reset();
            }
        }
    }
}
