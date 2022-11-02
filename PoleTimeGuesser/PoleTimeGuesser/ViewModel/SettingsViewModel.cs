using PoleTimeGuesser.Services.UpdateUser;
using System.ComponentModel.DataAnnotations;

namespace PoleTimeGuesser.ViewModel
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        ISharedData _sharedData;
        [ObservableProperty]
        bool isProcessing;
        [ObservableProperty]
        string _newUsername;
        [ObservableProperty]
        string _newEmail;
        [ObservableProperty]
        string _newPassword;
        ServiceManager _serviceManager;

        public SettingsViewModel(ISharedData sharedData, ServiceManager serviceManager)
        {
            NewUsername = NewPassword = NewEmail = "";
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
                if (!new EmailAddressAttribute().IsValid(_newEmail) && !string.IsNullOrEmpty(_newEmail))
                    throw new Exception("Wrong email!");

                var request = new UpdateRequest
                {
                    Id = _sharedData.Id,
                    Username = _newUsername,
                    Password = _newPassword,
                    Email = _newEmail,
                };

                var response = await _serviceManager.UpdateUser(request);

                if (response.StatusCode == 200)
                {
                    _sharedData.Username = response.Username;
                    await AppShell.Current.DisplayAlert("F1Guess", "Succesfull!", "OK");
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
        async Task LogOut()
        {
            _sharedData.Email = "";
            _sharedData.Username = "";
            _sharedData.AvatarSourceName = "";
            Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync("///LoginView");

            // TODO: Még nem tökéletes a kilépés. Megooldani hogy minden oldal újra töltsön.
        }
    }
}
