namespace PoleTimeGuesser.ViewModel
{
    [QueryProperty("Constructor", "Constructor")]
    public partial class ConstructorDetailsViewModel : BaseViewModel
    {
        private readonly IF1DataGetterService _f1DataGetterService;
        [ObservableProperty]
        ConstructorModel _constructor;

        public ConstructorDetailsViewModel(IF1DataGetterService f1DataGetterService)
        {
            _f1DataGetterService = f1DataGetterService;
            IsBusy = true;
            PageState = pStates.Loading.ToString();
        }

        public void Initailize()
        {
            try
            {
                Task.Run(async () =>
                {
                    //await GetCircuitInfo();
                }).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                PageState = pStates.Error.ToString();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
