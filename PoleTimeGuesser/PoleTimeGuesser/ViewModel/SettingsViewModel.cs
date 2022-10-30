namespace PoleTimeGuesser.ViewModel
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        ISharedData _sharedData;
        [ObservableProperty]
        bool isProcessing;
        ServiceManager _serviceManager;

        public SettingsViewModel(ISharedData sharedData, ServiceManager serviceManager)
        {
            IsProcessing = false;
            _sharedData = sharedData;
            _serviceManager = serviceManager;
        }

        [RelayCommand]
        async Task UpdateUser()
        {
            if (IsProcessing) return;
            IsProcessing = true;
            try
            {
                // TODO : Finish the logic
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("F1Guess", ex.Message, "OK");
            }
            finally
            {
                IsProcessing = false;
            }
        }

        [RelayCommand]
        async Task LogOut()
        {
            Debug.WriteLine(_sharedData.Email);
            _sharedData.Email = "";
            _sharedData.Username = "";
            _sharedData.AvatarSourceName = "";
            Debug.WriteLine(_sharedData.Email);
            await Shell.Current.GoToAsync(nameof(LoginView));
        }
    }
}
