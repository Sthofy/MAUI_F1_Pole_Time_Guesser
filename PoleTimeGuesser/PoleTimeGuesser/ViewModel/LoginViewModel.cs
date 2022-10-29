namespace PoleTimeGuesser.ViewModel
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        string _username;

        [ObservableProperty]
        string _password;
        [ObservableProperty]
        bool _isProcessing;

        ServiceManager _serviceManager;

        public LoginViewModel(ServiceManager serviceManager)
        {
            IsProcessing = false;
            _serviceManager = serviceManager;
        }

        [RelayCommand]
        async Task Login()
        {
            //await Shell.Current.GoToAsync($"{nameof(CircuitDetailsView)}", true,
            //    new Dictionary<string, object>
            //    {
            //        { "Circuit" , schedule },
            //    });


            //await Shell.Current.GoToAsync("///main");

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
                if(response.StatusCode==200)
                {
                    await AppShell.Current.DisplayAlert("F1Guess", "Login sucessful!\n" +
                        $"Username: {response.Username}\n" +
                        $"Email: {response.Email}\n"+
                        $"AvatarSourceName: {response.AvatarSourceName}", "OK");
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

            }
        }

        [RelayCommand]
        async Task GoToRegisterPage()
        {
            await Shell.Current.GoToAsync("///RegisterPage");
        }
    }
}
