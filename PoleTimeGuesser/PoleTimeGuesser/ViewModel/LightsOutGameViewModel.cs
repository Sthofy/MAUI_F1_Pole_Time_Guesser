namespace PoleTimeGuesser.ViewModel
{
    public partial class LightsOutGameViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _header;

        public LightsOutGameViewModel()
        {
            Header = "Can you faster then F1 Driver?\n Let's try!";
        }
    }
}
