using System.Text.Json;
using WeatherApp.BusinessLogic;

namespace WeatherApp.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private void LoginButton_Clicked(object sender, EventArgs e)
    {
        if (Username.Text != null)
        {
            JsonManager manager = new JsonManager(Username.Text);
            Navigation.PushAsync(new MainPage(manager));
        }
    }
}