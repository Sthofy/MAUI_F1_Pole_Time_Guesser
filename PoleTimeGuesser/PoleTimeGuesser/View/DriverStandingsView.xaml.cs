namespace PoleTimeGuesser.View;

public partial class DriverStandingsView : ContentPage
{
    public DriverStandingsView(DriverStandingsViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }
}