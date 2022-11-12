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
        [ObservableProperty]
        string _lenTextColor = "red";
        [ObservableProperty]
        string _lTextColor = "red";
        [ObservableProperty]
        string _uTextColor = "red";
        [ObservableProperty]
        string _nTextColor = "red";

        private bool _isStrongPassword = false;
        IServiceManager _serviceManager;

        public RegisterViewModel(IServiceManager serviceManager)
        {
            IsProcessing = false;
            _serviceManager = serviceManager;
        }

        [RelayCommand]
        async Task Register()
        {
            //TODO: Megvizsgálni hogy létezik e már a felhasználó
            //TODO: Létrehozáskor beszúrni a scoreboard táblába 0 ponttal

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


                if (_isStrongPassword)
                {
                    var request = new RegistrationRequest
                    {
                        Username = Username,
                        Password = Password,
                        Email = Email,
                    };

                    await _serviceManager.Registration(request);

                    await Shell.Current.GoToAsync($"{nameof(LoginView)}", true,
                        new Dictionary<string, object>
                        {
                            { "Username" , Username },
                            { "Password" , Password },
                        });
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
        public void VerifyPassword()
        {
            bool lenght = false, lower = false, upper = false, number = false;
            if (Password.Length > 8)
            {
                LenTextColor = "Green";
                lenght = true;
            }
            else
            {
                LenTextColor = "Red";
                lenght = false;
            }
            if (Password.Any(char.IsLower))
            {
                LTextColor = "Green";
                lower = true;
            }
            else
            {
                LTextColor = "Red";
                lower = false;
            }
            if (Password.Any(char.IsUpper))
            {
                UTextColor = "Green";
                upper = true;
            }
            else
            {
                UTextColor = "Red";
                upper = false;
            }
            if (Password.Any(char.IsNumber))
            {
                NTextColor = "Green";
                number = true;
            }
            else
            {
                NTextColor = "Red";
                number = false;
            }

            if (lenght && lower && upper && number)
                _isStrongPassword = true;
        }

        private bool VerifyUsername()
        {
            var request = new GetByUsernameRequest
            {
                Username = Username,
            };
            var response = _serviceManager.CallWebAPI<GetByUsernameRequest, GetByUsernameResponse>("/User/GetByUsername", HttpMethod.Get, request).Result;

            return response.IsExist;
        }
    }
}
