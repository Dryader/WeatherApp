using System.Text.Json;
using WeatherApp.BusinessLogic;
using Geocoding = WeatherApp.BusinessLogic.GeocodingLogic;

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
        // var weatherDataTask = CurrentWeather.GetWeatherData(43.7, -79.42);
        //
        // Temperature.Text = weatherDataTask.Result.temp.ToString();
        // TempIcon.Source = weatherDataTask.Result.iconUrl;
        // Description.Text = weatherDataTask.Result.description;
        // Humidity.Text = weatherDataTask.Result.weatherData.main.humidity.ToString();
        // WindLocal.Text = weatherDataTask.Result.weatherData.wind.speed.ToString();
    }

    private void ViewForecastButton_Clicked(object sender, EventArgs e)
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
        Temperature.Text = "Temperature: " + weatherData.main.temp + "°C";
        TempIcon.Source = $"https://openweathermap.org/img/wn/{weatherData.weather[0].icon}@2x.png";
        ;
        Description.Text = weatherData.weather[0].description;
        Humidity.Text = "Humidity: " + weatherData.main.humidity + "%";
        WindLocal.Text = "Wind: " + weatherData.wind.speed + "km/h";
    }

    private async void GetLocationDataUsingPostalCode()
    {
        var postalCode = CityEntry.Text;
        var url =
            $"https://api.openweathermap.org/geo/1.0/zip?zip={postalCode},CA&appid=af814f7c81ec8ac0ad157b953140d72e";
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var data = await response.Content.ReadAsStringAsync();
        var locationData = JsonSerializer.Deserialize<GeocodingLogic.RootObject>(data);
        GetWeatherData(locationData.lat, locationData.lon);
        CityEntry.Text = locationData.name;
    }
}