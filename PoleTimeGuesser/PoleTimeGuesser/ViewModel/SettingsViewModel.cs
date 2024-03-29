﻿using System.ComponentModel.DataAnnotations;

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
        IServiceManager _serviceManager;

        public SettingsViewModel(ISharedData sharedData, IServiceManager serviceManager)
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

                var request = new UpdateUserRequest
                {
                    Id = _sharedData.Id,
                    Username = _newUsername,
                    Password = _newPassword,
                    Email = _newEmail,
                };

                var response = await _serviceManager.UpdateUser(request);

                if (response.IsSuccessStatusCode)
                {
                    // TODO: _shared data lekérni újra az adatokat
                    await AppShell.Current.DisplayAlert("F1Guess", "Succesfull!", "OK");
                }
                else
                {
                    await AppShell.Current.DisplayAlert("F1Guess", "Failed!", "OK");
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
        }
    }
}
