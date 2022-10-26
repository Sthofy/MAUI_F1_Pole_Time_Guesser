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
            .AddSingleton<LoginViewModel>();

        builder.Services.AddSingleton<MainPage>()
            .AddTransient<DriverDetailsView>()
            .AddSingleton<ScheduleView>()
            .AddTransient<CircuitDetailsView>()
            .AddSingleton<LoginView>();

        return builder.Build();
    }
}
