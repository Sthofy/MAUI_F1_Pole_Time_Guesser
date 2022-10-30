namespace PoleTimeGuesser.ViewModel
{
    [QueryProperty("Username", "Username")]
    [QueryProperty("Password", "Password")]
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        string _username;

        [ObservableProperty]
        string _password;
        [ObservableProperty]
        bool _isProcessing;

        ServiceManager _serviceManager;
        ISharedData _sharedData;

        public LoginViewModel(ServiceManager serviceManager, ISharedData sharedData)
        {
            Username = "Sthofy";
            Password = "Pwd12345.";
            IsProcessing = false;
            _serviceManager = serviceManager;
            _sharedData = sharedData;
        }

        [RelayCommand]
        async Task Login()
        {
            if (IsProcessing) return;

            IsProcessing = true;

            try
            {
                if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                    throw new Exception("Usernaem or Password field is empty!");

                var requset = new AuthenticateRequest
                {
                    Username = Username,
                    Password = Password,
                };

                var response = await _serviceManager.Authenticate(requset);
                if (response.StatusCode == 200)
                {
                    _sharedData.Username = response.Username;
                    _sharedData.AvatarSourceName = response.AvatarSourceName;
                    _sharedData.Email = response.Email;
                    await Shell.Current.GoToAsync("///main");
                }
                else
                {
                    await AppShell.Current.DisplayAlert("F1Guess", response.StatusMessage, "OK");
                }
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
        async Task GoToRegisterPage()
        {
            await Shell.Current.GoToAsync("///RegisterPage");
        }
    }
}
