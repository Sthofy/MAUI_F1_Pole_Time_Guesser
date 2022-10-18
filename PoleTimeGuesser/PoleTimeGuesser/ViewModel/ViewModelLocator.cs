namespace PoleTimeGuesser.ViewModel
{
    public class ViewModelLocator
    {
        public DriverStandingsViewModel DriverStandingsView => new DriverStandingsViewModel();
        public ScheduleViewModel ScheduleView => new ScheduleViewModel();
        public DriverDetailsViewModel DriverDetailsView => new DriverDetailsViewModel();
        public CircuitDetailsViewModel CircuitDetailsView => new CircuitDetailsViewModel();
    }

}
