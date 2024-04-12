using System.Text.Json;
using WeatherApp.BusinessLogic;

namespace WeatherApp.Pages;

public partial class FiveDayForecast : ContentPage
{
    public FiveDayForecast()
    {
        InitializeComponent();
    }


    private async void GetFiveDayForecast(double latitude, double longitude)
    {
        var url =
            $"https://api.openweathermap.org/data/2.5/onecall?lat={latitude}&lon={longitude}&&appid=af814f7c81ec8ac0ad157b953140d72e&units=metric";
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var data = await response.Content.ReadAsStringAsync();
        var ForecastData = JsonSerializer.Deserialize<fiveDayForecast.RootObject>(data);
    }

    // private async void GetLocationDataUsingPostalCode()
    // {
    //     var url =
    //         $"https://api.openweathermap.org/geo/1.0/zip?zip={CityEntry.Text},CA&appid=af814f7c81ec8ac0ad157b953140d72e";
    //     var client = new HttpClient();
    //     var response = await client.GetAsync(url);
    //     var data = await response.Content.ReadAsStringAsync();
    //     var locationData = JsonSerializer.Deserialize<Geocoding.RootObject>(data);
    //     GetFiveDayForecast(locationData.lat, locationData.lon);
    //     CityEntry.Text = locationData.name;
    // }
}