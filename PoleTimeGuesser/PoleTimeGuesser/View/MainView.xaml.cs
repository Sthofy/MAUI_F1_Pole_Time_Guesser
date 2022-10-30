namespace PoleTimeGuesser.View;

public partial class MainView : ContentPage
{
    public MainView(MainViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }

}

