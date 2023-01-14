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
            Init = Initailize();
        }

        public async Task Initailize()
        {
            await GetConstructorStandingsAsync();
        }

        [RelayCommand]
        async Task GetConstructorStandingsAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var constructors = await _f1DataGetterService.GetConstructorStandings();
                if (Constructors.Count != 0)
                    Constructors.Clear();

                foreach (var item in constructors)
                {
                    Constructors.Add(item);
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
        async Task GoToConstructorDetailsAsync(ConstructorStandingsModel model)
        {
            if (model is null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ConstructorDetailsView)}", true,
                new Dictionary<string, object>
                {
                    { "Constructor" , model },
                });
        }
    }
}
