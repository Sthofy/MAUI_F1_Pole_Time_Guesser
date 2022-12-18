namespace PoleTimeGuesser;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(DriverDetailsView), typeof(DriverDetailsView));
        Routing.RegisterRoute(nameof(CircuitDetailsView), typeof(CircuitDetailsView));
        Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
        Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
        Routing.RegisterRoute(nameof(SettingsView), typeof(SettingsView));
        Routing.RegisterRoute(nameof(QuizView), typeof(QuizView));
        Routing.RegisterRoute(nameof(GuessView), typeof(GuessView));
        Routing.RegisterRoute(nameof(LightsOutGameView), typeof(LightsOutGameView));
        Routing.RegisterRoute(nameof(ConstructorStandingsView), typeof(ConstructorStandingsView));
    }
}
