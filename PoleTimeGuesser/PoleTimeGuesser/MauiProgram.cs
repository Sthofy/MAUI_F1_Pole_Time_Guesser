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
           .AddScoped<LoginViewModel>()
           .AddTransient<RegisterViewModel>()
           .AddTransient<MainViewModel>()
           .AddScoped<SettingsViewModel>()
           .AddScoped<GamesViewModel>()
           .AddTransient<GuessViewModel>()
           .AddTransient<QuizViewModel>()
           .AddScoped<LightsOutGameViewModel>()
           .AddTransient<ConstructorStandingsViewModel>()
           .AddTransient<ConstructorDetailsViewModel>();
        #endregion

        #region Views
        builder.Services.AddTransient<MainView>()
            .AddTransient<DriverStandingsView>()
            .AddTransient<ScheduleView>()
            .AddTransient<DriverDetailsView>()
            .AddTransient<CircuitDetailsView>()
            .AddScoped<LoginView>()
            .AddTransient<RegisterView>()
            .AddScoped<SettingsView>()
            .AddScoped<GamesView>()
            .AddTransient<GuessView>()
            .AddTransient<QuizView>()
            .AddScoped<LightsOutGameView>()
            .AddTransient<ConstructorStandingsView>()
            .AddTransient<ConstructorDetailsView>();
        #endregion

        #region Services
        builder.Services.AddScoped<IServiceManager, ServiceManager>()
            .AddScoped<ISharedData, SharedData>()
            .AddScoped<IF1DataGetterService, F1DataGetterService>();
        #endregion

        return builder.Build();
    }
}
