namespace PoleTimeGuesser.View;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}