namespace PoleTimeGuesser.ViewModel
{
    public partial class GuessViewModel : ObservableObject
    {
        [ObservableProperty]
        string _gameRules;

        public GuessViewModel()
        {
            GameRules = "Szia!" +
                "\n\nA Tipp játék során a feldatod egyszerű." +
                "\n\nTippeld meg ki nyeri az időmérőt és milyen idővel szerzi meg azt!";
        }
    }
}
