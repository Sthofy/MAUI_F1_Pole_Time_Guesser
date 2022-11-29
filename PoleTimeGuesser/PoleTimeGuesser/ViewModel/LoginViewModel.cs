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

        IServiceManager _serviceManager;
        ISharedData _sharedData;

        public LoginViewModel(IServiceManager serviceManager, ISharedData sharedData)
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
                    throw new Exception("Username or Password field is empty!");

                var request = new AuthenticateRequest
                {
                    Username = Username,
                    Password = Password,
                };

                var response = await _serviceManager.Authenticate(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<LoggedInUserModel>(responseContent);

                    _sharedData.Id = result.Id;
                    _sharedData.Username = result.Username;
                    _sharedData.AvatarSourceName = result.AvatarSourceName;
                    _sharedData.Email = result.Email;
                    _sharedData.Token = result.Token;

                    await Shell.Current.GoToAsync("///main");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await Shell.Current.DisplayAlert("F1Guess", "Username or Password is wrong!", "Ok");
                }
                else
                {
                    await Shell.Current.DisplayAlert(response.StatusCode.ToString(), response.ReasonPhrase, "Ok");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("F1Guess", ex.Message, "Ok");
            }
            finally
            {
                IsProcessing = false;
            }
        }

        [RelayCommand]
        async Task GoToRegisterPage()
        {
            await Shell.Current.GoToAsync("///RegisterPage", true);
        }
    }
}
