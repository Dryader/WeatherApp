namespace WeatherApp;

public partial class MainPage : ContentPage
{
    private int count;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
    // https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid=af814f7c81ec8ac0ad157b953140d72e

    private async void OnWeatherClicked(object sender, EventArgs e)
    {
        var url =
            "https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid=af814f7c81ec8ac0ad157b953140d72e";
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var data = await response.Content.ReadAsStringAsync();
        WeatherLabel.Text = data;
    }
}