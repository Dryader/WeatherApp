using System.Text.Json;

namespace WeatherApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        GetLocationDataUsingPostalCode();
    }


    private async void GetWeatherData(double latitude, double longitude)
    {
        var url =
            $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid=af814f7c81ec8ac0ad157b953140d72e";
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var data = await response.Content.ReadAsStringAsync();
    }

    private async void GetLocationDataUsingPostalCode()
    {
        var postalCode = cityEntry.Text;
        var url =
            $"http://api.openweathermap.org/geo/1.0/direct?q={postalCode}&limit=5&appid=af814f7c81ec8ac0ad157b953140d72e";
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var data = await response.Content.ReadAsStringAsync();
        var locationData = JsonSerializer.Deserialize<List<LocationData>>(data);
        cityEntry.Text = locationData[0].name;
    }

    private class LocationData
    {
        public string zip { get; set; }
        public string name { get; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
    }
}