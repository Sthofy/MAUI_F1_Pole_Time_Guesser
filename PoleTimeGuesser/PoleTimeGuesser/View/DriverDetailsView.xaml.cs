namespace PoleTimeGuesser.View;

public partial class DriverDetailsView : ContentPage
{
    public DriverDetailsView(DriverDetailsViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        (this.BindingContext as DriverDetailsViewModel).Initailize();
    }
}