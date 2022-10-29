namespace PoleTimeGuesser.View;

public partial class RegisterView : ContentPage
{
	public RegisterView(RegisterViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}