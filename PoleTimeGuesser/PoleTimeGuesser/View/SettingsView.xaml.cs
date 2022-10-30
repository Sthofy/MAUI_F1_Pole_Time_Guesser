namespace PoleTimeGuesser.View;

public partial class SettingsView : ContentPage
{
    public SettingsView(SettingsViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }
}