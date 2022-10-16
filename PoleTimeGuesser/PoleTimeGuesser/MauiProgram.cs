namespace PoleTimeGuesser;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddSingleton<F1DataGetterService>()
			.AddSingleton<DriverStandingsViewModel>()
			.AddTransient<DriverDetailsViewModel>();

		builder.Services.AddSingleton<MainPage>()
			.AddSingleton<DriverDetailsView>();

		return builder.Build();
	}
}
