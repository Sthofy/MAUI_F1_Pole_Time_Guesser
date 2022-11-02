namespace PoleTimeGuesser.View;

public partial class QuizView : ContentPage
{
    public QuizView(QuizViewModel vm)
    {
        InitializeComponent();
        this.BindingContext = vm;
    }
}