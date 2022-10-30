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

        builder.Services.AddSingleton<F1DataGetterService>()
            .AddSingleton<DriverStandingsViewModel>()
            .AddTransient<DriverDetailsViewModel>()
            .AddSingleton<ScheduleViewModel>()
            .AddTransient<CircuitDetailsViewModel>()
            .AddSingleton<LoginViewModel>()
            .AddSingleton<RegisterViewModel>()
            .AddSingleton<MainViewModel>()
            .AddSingleton<SettingsViewModel>();

        builder.Services.AddSingleton<MainView>()
            .AddTransient<DriverDetailsView>()
            .AddSingleton<ScheduleView>()
            .AddTransient<CircuitDetailsView>()
            .AddSingleton<LoginView>()
            .AddSingleton<RegisterView>()
            .AddSingleton<SettingsView>();

        builder.Services.AddSingleton<ServiceManager>()
            .AddSingleton<ISharedData,SharedData>();

        return builder.Build();
    }
}
