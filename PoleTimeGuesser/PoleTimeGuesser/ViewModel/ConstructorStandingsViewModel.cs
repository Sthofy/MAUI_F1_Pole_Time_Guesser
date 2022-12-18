namespace PoleTimeGuesser.ViewModel
{
    public partial class ConstructorStandingsViewModel : BaseViewModel
    {
        private readonly IF1DataGetterService _f1DataGetterService;
        private readonly IServiceManager _serviceManager;

        public ObservableCollection<ConstructorStandingsModel> Constructors { get; } = new();
        public Task Init { get; }

        public ConstructorStandingsViewModel(IF1DataGetterService f1DataGetterService, IServiceManager serviceManager)
        {
            _f1DataGetterService = f1DataGetterService;
            _serviceManager = serviceManager;
            IsBusy = true;
            PageState = pStates.Loading.ToString();
            Init = Initailize();
        }

        public async Task Initailize()
        {
            await GetConstructorStandingsAsync();
        }

        [RelayCommand]
        async Task GetConstructorStandingsAsync()
        {
            try
            {
                var constructors = await _f1DataGetterService.GetConstructorStandings();
                if (Constructors.Count != 0)
                    Constructors.Clear();

                constructors.ForEach(x => Constructors.Add(x));

                PageState = pStates.Success.ToString();
            }
            catch (Exception ex)
            {
                PageState = pStates.Error.ToString();
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
