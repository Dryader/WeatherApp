using System.Text.Json;
using WeatherApp.BusinessLogic;

namespace WeatherApp.Pages;

public partial class FiveDayForecast : ContentPage
{
    public FiveDayForecast()
    {
        InitializeComponent();

        GetFiveDayForecast(43.7, -79.42);
    }


    private async void GetFiveDayForecast(double latitude, double longitude)
    {
        var url =
            $"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&exclude=current,minutely,hourly&appid=af814f7c81ec8ac0ad157b953140d72e&units=metric";
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var data = await response.Content.ReadAsStringAsync();
        var ForecastData = JsonSerializer.Deserialize<fiveDayForecast.RootObject>(data);
    }
}