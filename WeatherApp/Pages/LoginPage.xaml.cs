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

        string username = Username.Text;
        string fileName = "UserInfo.txt";
        string path = Path.Combine(Environment.CurrentDirectory, fileName);
        File.AppendAllText(path, username);
    }
}