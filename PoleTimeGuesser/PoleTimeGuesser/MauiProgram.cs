using PoleTimeGuesser.Model;
using PoleTimeGuesser.Services;
using PoleTimeGuesser.View;
using PoleTimeGuesser.ViewModel;

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

		builder.Services.AddSingleton<IF1DataGetterService, F1DataGetterService>()
			.AddSingleton<DriverStandingsViewModel>();

		builder.Services.AddSingleton<MainPage>();

		return builder.Build();
	}
}
