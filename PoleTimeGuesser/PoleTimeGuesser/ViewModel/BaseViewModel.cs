namespace PoleTimeGuesser.ViewModel
{
    enum pStates { Loading, Error, Success }
    public partial class BaseViewModel : ObservableObject
    {
        public BaseViewModel() { }

        [ObservableProperty]
        string title;

        [ObservableProperty]
        string _pageState;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        public bool IsNotBusy => !isBusy;
    }
}
