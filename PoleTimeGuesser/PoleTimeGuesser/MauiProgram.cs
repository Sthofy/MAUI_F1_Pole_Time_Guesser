namespace PoleTimeGuesser;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddSingleton<DriverStandingsViewModel>()
            .AddSingleton<DriverDetailsViewModel>()
            .AddSingleton<ScheduleViewModel>()
            .AddSingleton<CircuitDetailsViewModel>()
            .AddSingleton<LoginViewModel>()
            .AddSingleton<RegisterViewModel>()
            .AddTransient<MainViewModel>()
            .AddSingleton<SettingsViewModel>()
            .AddSingleton<GamesViewModel>()
            .AddTransient<GuessViewModel>()
            .AddTransient<QuizViewModel>()
            .AddSingleton<LightsOutGameViewModel>();

        builder.Services.AddTransient<MainView>()
            .AddTransient<DriverStandingsView>()
            .AddTransient<ScheduleView>()
            .AddTransient<DriverDetailsView>()
            .AddSingleton<CircuitDetailsView>()
            .AddSingleton<LoginView>()
            .AddTransient<RegisterView>()
            .AddSingleton<SettingsView>()
            .AddSingleton<GamesView>()
            .AddTransient<GuessView>()
            .AddTransient<QuizView>()
            .AddSingleton<LightsOutGameView>();

        builder.Services.AddSingleton<IServiceManager, ServiceManager>()
            .AddSingleton<ISharedData, SharedData>()
            .AddSingleton<IF1DataGetterService, F1DataGetterService>();

        return builder.Build();
    }
}
