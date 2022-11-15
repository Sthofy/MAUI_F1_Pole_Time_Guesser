namespace PoleTimeGuesser.ViewModel
{
    public partial class GamesViewModel : ObservableObject
    {
        [RelayCommand]
        async Task ShowLightOutPage()
        {
            await Shell.Current.GoToAsync(nameof(LightsOutGameView));
        }


        [RelayCommand]
        async Task ShowQuizPage()
        {
            await Shell.Current.GoToAsync(nameof(QuizView));
        }

        [RelayCommand]
        async Task ShowGuessPage()
        {
            await Shell.Current.GoToAsync(nameof(GuessView));
        }

    }
}
