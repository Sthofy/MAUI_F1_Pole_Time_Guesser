namespace PoleTimeGuesser.ViewModel
{
    [QueryProperty("Circuit", "Circuit")]
    public partial class CircuitDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        ScheduleModel circuit;

    }
}
