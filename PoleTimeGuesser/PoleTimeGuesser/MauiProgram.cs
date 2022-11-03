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
            .AddTransient<DriverDetailsViewModel>()
            .AddSingleton<ScheduleViewModel>()
            .AddTransient<CircuitDetailsViewModel>()
            .AddSingleton<LoginViewModel>()
            .AddSingleton<RegisterViewModel>()
            .AddTransient<MainViewModel>()
            .AddSingleton<SettingsViewModel>()
            .AddSingleton<GamesViewModel>()
            .AddSingleton<GuessViewModel>()
            .AddSingleton<QuizViewModel>();

        builder.Services.AddTransient<MainView>()
            .AddTransient<DriverStandingsView>()
            .AddTransient<ScheduleView>()
            .AddTransient<DriverDetailsView>()
            .AddTransient<CircuitDetailsView>()
            .AddSingleton<LoginView>()
            .AddTransient<RegisterView>()
            .AddSingleton<SettingsView>()
            .AddSingleton<GamesView>()
            .AddSingleton<GuessView>()
            .AddSingleton<QuizView>();

        builder.Services.AddSingleton<IServiceManager, ServiceManager>()
            .AddSingleton<ISharedData, SharedData>()
            .AddSingleton<IF1DataGetterService, F1DataGetterService>();

        return builder.Build();
    }
}
