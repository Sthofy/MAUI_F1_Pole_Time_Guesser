namespace PoleTimeGuesser.View;

public partial class CircuitDetailsView : ContentPage
{
    public CircuitDetailsView(CircuitDetailsViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        (this.BindingContext as CircuitDetailsViewModel).Initailize();
    }
}