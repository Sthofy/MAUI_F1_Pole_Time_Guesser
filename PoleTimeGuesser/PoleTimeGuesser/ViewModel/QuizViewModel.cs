namespace PoleTimeGuesser.ViewModel
{
    public partial class QuizViewModel : ObservableObject
    {
        [ObservableProperty]
        string _gameRules;

        [ObservableProperty]
        string _question;
        // TODO: QuestionModel létrehozása Question ans1 ans2 ans3 correctAns
        public ObservableCollection<Answear> quizAnswears { get; } = new();

        public QuizViewModel()
        {
            GameRules = "Szia!" +
                "\n\nA Quiz játék során a feldatod egyszerű." +
                "\n\nFelteszünk neked egy kérdést és neked csupán annyi a feladatod, hogy kiválaszd a helyes megfejtést." +
                "\n\nMinden nap lehetőséged lesz megválaszolni egy kérdést. A helyes megfejtés után 1000 pontot kapsz";

            Question = "Ki nyerte a 2022-es világbajnokságot";
            quizAnswears.Add(new Answear { Item = "Lewis Hamilton" });
            quizAnswears.Add(new Answear { Item = "Max Verstappen" });
            quizAnswears.Add(new Answear { Item = "George Russel" });
        }

        async Task GetRandomQuestion()
        {
            // TODO: Kérdés adatbázis létrehozása.
        }

        [RelayCommand]
        async Task VerifyAnswear(Answear answear)
        {
            // TODO:  A kapott választ összegasinlítai a jóval
            await Shell.Current.DisplayAlert("Válasz", $"{answear.Item}", "OK");
        }
    }
}
