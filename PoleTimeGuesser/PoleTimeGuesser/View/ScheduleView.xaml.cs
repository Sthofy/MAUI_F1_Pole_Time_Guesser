namespace PoleTimeGuesser.View;

public partial class ScheduleView : ContentPage
{
    public ScheduleView(ScheduleViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }
}