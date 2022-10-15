using PoleTimeGuesser.ViewModel;

namespace PoleTimeGuesser.View;

public partial class MainPage : ContentPage
{
	public MainPage(DriverStandingsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}

