namespace PoleTimeGuesser.View;

public partial class DriverDetailsView : ContentPage
{
    public DriverDetailsView(DriverDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}