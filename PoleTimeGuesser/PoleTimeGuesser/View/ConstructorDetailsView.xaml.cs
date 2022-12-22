namespace PoleTimeGuesser.View;

public partial class ConstructorDetailsView : ContentPage
{
    public ConstructorDetailsView(ConstructorDetailsViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        (this.BindingContext as CircuitDetailsViewModel).Initailize();
    }
}