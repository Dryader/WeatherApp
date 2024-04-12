using System.Text.Json;

namespace WeatherApp.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void WeatherButton_Clicked(object sender, EventArgs e)
    {
        GetLocationDataUsingPostalCode();
    }

    private void ViewForecastButton_Clicked(Object sender, EventArgs e)
    {
        Navigation.PushAsync(new FiveDayForecast());
    }

    private async void GetWeatherData(double latitude, double longitude)
    {
        var url =
            $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid=af814f7c81ec8ac0ad157b953140d72e&units=metric";
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var data = await response.Content.ReadAsStringAsync();
        var weatherData = JsonSerializer.Deserialize<CurrentWeather.RootObject>(data);
        var temp = weatherData.main.temp;
        Temperature.Text = temp.ToString();
        var description = weatherData.weather[0].description;
        var icon = weatherData.weather[0].icon;
        var iconUrl = $"https://openweathermap.org/img/wn/{icon}@2x.png";
        TempIcon.Source = iconUrl;
        Description.Text = description;
        Humidity.Text = weatherData.main.humidity.ToString();
        WindLocal.Text = weatherData.wind.speed.ToString();
    }

    private async void GetLocationDataUsingPostalCode()
    {
        var postalCode = CityEntry.Text;
        var url =
            $"https://api.openweathermap.org/geo/1.0/zip?zip={postalCode},CA&appid=af814f7c81ec8ac0ad157b953140d72e";
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var data = await response.Content.ReadAsStringAsync();
        var locationData = JsonSerializer.Deserialize<Geocoding.RootObject>(data);
        GetWeatherData(locationData.lat, locationData.lon);
        CityEntry.Text = locationData.name;
    }
}