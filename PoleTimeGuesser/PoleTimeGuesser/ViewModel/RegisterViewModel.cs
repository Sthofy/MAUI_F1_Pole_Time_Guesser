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
        [ObservableProperty]
        string _usernameTextColor = "Green";
        [ObservableProperty]
        string _errorMessage;

        private bool _isStrongPassword = false;
        private bool _isUserExist = false;
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


                if (_isStrongPassword && !_isUserExist)
                {
                    var request = new RegistrationRequest
                    {
                        Username = Username,
                        Password = Password,
                        Email = Email,
                    };

                    var userId = await _serviceManager.Registration(request);

                    InsertScore(userId.Id);

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

        [RelayCommand]
        private async Task VerifyUsername()
        {
            var response = await _serviceManager.CallWebAPI<string, GetByUsernameResponse>("/User/GetByUsername", HttpMethod.Post, Username);

            if (response.IsExist)
            {
                UsernameTextColor = "Red";
                _isUserExist = true;
                ErrorMessage = "Username already exists!";
            }
            else
            {
                UsernameTextColor = "Green";
                _isUserExist = false;
                ErrorMessage = "";
            }
        }

        [RelayCommand]
        private async Task BackToLoginPage()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginView)}", true);
        }

        private void InsertScore(int userId)
        {
            
            var request = new ScoreRequest
            {
                UserId = userId,
                Score = 0
            };
            var response = _serviceManager.CallWebAPI<ScoreRequest, BaseResponse>("/Game/InsertScore", HttpMethod.Post, request);
        }
    }
}
