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
                fonts.AddFont("coolvetica rg.otf", "CoolveticaRegular");
            });
        #region ViewModels
        builder.Services.AddScoped<DriverStandingsViewModel>()
           .AddTransient<DriverDetailsViewModel>()
           .AddScoped<ScheduleViewModel>()
           .AddTransient<CircuitDetailsViewModel>()
           .AddSingleton<LoginViewModel>()
           .AddSingleton<RegisterViewModel>()
           .AddTransient<MainViewModel>()
           .AddSingleton<SettingsViewModel>()
           .AddSingleton<GamesViewModel>()
           .AddTransient<GuessViewModel>()
           .AddTransient<QuizViewModel>()
           .AddSingleton<LightsOutGameViewModel>()
           .AddTransient<ConstructorStandingsViewModel>()
           .AddTransient<ConstructorDetailsViewModel>();
        #endregion

        #region Views
        builder.Services.AddTransient<MainView>()
            .AddTransient<DriverStandingsView>()
            .AddTransient<ScheduleView>()
            .AddTransient<DriverDetailsView>()
            .AddTransient<CircuitDetailsView>()
            .AddSingleton<LoginView>()
            .AddTransient<RegisterView>()
            .AddSingleton<SettingsView>()
            .AddSingleton<GamesView>()
            .AddTransient<GuessView>()
            .AddTransient<QuizView>()
            .AddSingleton<LightsOutGameView>()
            .AddTransient<ConstructorStandingsView>()
            .AddTransient<ConstructorDetailsView>();
        #endregion

        #region Services
        builder.Services.AddSingleton<IServiceManager, ServiceManager>()
            .AddSingleton<ISharedData, SharedData>()
            .AddScoped<IF1DataGetterService, F1DataGetterService>();
        #endregion

        return builder.Build();
    }
}
