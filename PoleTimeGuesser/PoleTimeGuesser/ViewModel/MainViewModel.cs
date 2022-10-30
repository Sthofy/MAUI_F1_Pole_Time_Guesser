namespace PoleTimeGuesser.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        ISharedData _sharedData;

        public MainViewModel(ISharedData sharedData)
        {
            _sharedData = sharedData;
        }

        [RelayCommand]
        async Task GoToSettingsPage()
        {
            await Shell.Current.GoToAsync(nameof(SettingsView));
        }
    }
}
