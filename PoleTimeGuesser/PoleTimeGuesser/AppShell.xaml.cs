namespace PoleTimeGuesser;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(DriverDetailsView), typeof(DriverDetailsView));
        Routing.RegisterRoute(nameof(CircuitDetailsView), typeof(CircuitDetailsView));
        Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
    }
}
