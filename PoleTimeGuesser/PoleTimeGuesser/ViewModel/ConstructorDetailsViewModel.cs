namespace PoleTimeGuesser.ViewModel
{
    [QueryProperty("Constructor", "Constructor")]
    public partial class ConstructorDetailsViewModel : BaseViewModel
    {
        private readonly IF1DataGetterService _f1DataGetterService;
        [ObservableProperty]
        ConstructorStandingsModel _constructor;
        [ObservableProperty]
        ConstructorInfoModel _constructorInfo;

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
                    await GetConstructorInfo();
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

        private async Task GetConstructorInfo()
        {
            var result = await _f1DataGetterService.GetConstructorInfoAsync(Constructor.Constructor.constructorId);

            if(result is null)
            {
                PageState=pStates.Error.ToString();
            }
            else
            {
                ConstructorInfo = result;
                PageState=pStates.Success.ToString();
            }
        }
    }
}
