using System.ComponentModel.DataAnnotations;

namespace PoleTimeGuesser.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {
        [ObservableProperty]
        string _username;
        [ObservableProperty]
        string _email;
        [ObservableProperty]
        string _password;
        [ObservableProperty]
        string _confirmPassword;
        [ObservableProperty]
        bool isProcessing;

        ServiceManager _serviceManager;

        public RegisterViewModel(ServiceManager serviceManager)
        {
            IsProcessing = false;
            _serviceManager = serviceManager;
        }

        [RelayCommand]
        async Task Register()
        {
            if (IsProcessing) return;

            IsProcessing = true;

            try
            {
                if (Username.Trim() == "" || Password.Trim() == "" || ConfirmPassword.Trim() == "" || Email.Trim() == "")
                    throw new Exception("One or more field is empty");
                if (!new EmailAddressAttribute().IsValid(Email))
                    throw new Exception("Wrong email!");
                if (!Password.Equals(ConfirmPassword))
                    throw new Exception("Password and Confirm Password is not equal");

                var request = new RegistrationRequest
                {
                    Username = Username,
                    Password = Password,
                    Email = Email,
                };

                var response = await _serviceManager.Registration(request);

                if (response.StatusCode == 200)
                {
                    await Shell.Current.GoToAsync($"{nameof(LoginView)}", true,
                        new Dictionary<string, object>
                        {
                            { "Username" , Username },
                            { "Password" , Password },
                        });
                }
                else
                    await AppShell.Current.DisplayAlert("F1Guess", response.StatusMessage, "OK");
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
    }
}
