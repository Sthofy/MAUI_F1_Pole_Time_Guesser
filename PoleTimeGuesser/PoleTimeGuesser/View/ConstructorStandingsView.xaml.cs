namespace PoleTimeGuesser.View;

public partial class ConstructorStandingsView : ContentPage
{
    public ConstructorStandingsView(ConstructorStandingsViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }
}