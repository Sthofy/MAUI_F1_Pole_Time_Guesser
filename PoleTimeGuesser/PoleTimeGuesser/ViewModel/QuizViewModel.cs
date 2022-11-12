using PoleTimeGuesser.Model;
using PoleTimeGuesser.Services.Game;

namespace PoleTimeGuesser.ViewModel
{
    public partial class QuizViewModel : BaseViewModel
    {
        [ObservableProperty]
        string _gameRules;
        [ObservableProperty]
        string _question;
        [ObservableProperty]
        bool _isEnabledToClick;

        public Task Init { get; }
        public ObservableCollection<Answear> Answears { get; } = new();

        QuestionModel _questionModel;
        IServiceManager _serviceManager;
        ISharedData _sharedData;
        List<QuestionModel> ListOfQuestions = new List<QuestionModel>();
        string _corretAnswer = "";


        public QuizViewModel(IServiceManager serviceManager, ISharedData sharedData)
        {
            _serviceManager = serviceManager;
            _sharedData = sharedData;
            IsEnabledToClick = true;

            GameRules = "Szia!" +
                "\n\nA Quiz játék során a feldatod egyszerű." +
                "\n\nFelteszünk neked egy kérdést és neked csupán annyi a feladatod, hogy kiválaszd a helyes megfejtést." +
                "\n\nMinden nap lehetőséged lesz megválaszolni egy kérdést. A helyes megfejtés után 1000 pontot kapsz";

            Init = Initialize();
        }

        private async Task Initialize()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                await GetQuestions();
                GetRandomQuestion();
                ShuffleAnswers();
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

        async Task GetQuestions()
        {
            var response = await _serviceManager.CallWebAPI<int?, QuestionResponse>("/Game/Questions", HttpMethod.Get, null);

            ListOfQuestions = response.Questions;
        }


        private void GetRandomQuestion()
        {
            Random rnd = new Random();
            _questionModel = ListOfQuestions[rnd.Next(ListOfQuestions.Count)];

            Answears.Add(new Answear { Item = _questionModel.AnswerA });
            Answears.Add(new Answear { Item = _questionModel.AnswerB });
            Answears.Add(new Answear { Item = _questionModel.AnswerC });

            _corretAnswer = _questionModel.AnswerC;
            Question = _questionModel.Question;
        }

        private void ShuffleAnswers()
        {
            Random rnd = new Random();
            int n = Answears.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                var temp = Answears[k];
                Answears[k] = Answears[n];
                Answears[n] = temp;
            }
        }

        [RelayCommand]
        async Task VerifyAnswear(Answear answear)
        {
            // TODO: Gombra rakni a kiértékelést
            if (answear.Item.Equals(_corretAnswer))
            {
                var request = new ScoreRequest
                {
                    UserId = _sharedData.Id,
                    Score = 1000
                };
                var response = _serviceManager.CallWebAPI<ScoreRequest, BaseResponse>("/Game/InsertScore", HttpMethod.Post, request);
                IsEnabledToClick = false;
                await Shell.Current.DisplayAlert("Üzenet", "Helyes!", "OK");
            }
            else
                await Shell.Current.DisplayAlert("Üzenet", $"Sajnos rossz a helyes válasz {_corretAnswer}", "Ok");
        }
    }
}
