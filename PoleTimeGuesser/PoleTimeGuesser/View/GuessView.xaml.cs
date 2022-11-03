namespace PoleTimeGuesser.View;

public partial class GuessView : ContentPage
{
    public GuessView(GuessViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }
}