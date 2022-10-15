namespace PoleTimeGuesser.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        public BaseViewModel() { }

        [ObservableProperty]
        string title;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        public bool IsNotBusy => !isBusy;
    }
}
