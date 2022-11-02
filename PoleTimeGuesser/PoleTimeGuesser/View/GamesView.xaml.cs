namespace PoleTimeGuesser.View;

public partial class GamesView : ContentPage
{
    public GamesView(GamesViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }
}