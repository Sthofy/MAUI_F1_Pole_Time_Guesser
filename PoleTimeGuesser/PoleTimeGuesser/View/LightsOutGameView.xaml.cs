namespace PoleTimeGuesser.View;

public partial class LightsOutGameView : ContentPage
{
    public LightsOutGameView(LightsOutGameViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }
}