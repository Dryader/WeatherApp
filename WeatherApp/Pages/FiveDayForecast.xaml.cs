using System.Text.Json;
using WeatherApp.BusinessLogic;
using Geocoding = WeatherApp.BusinessLogic.GeocodingLogic;

namespace WeatherApp.Pages;

public partial class FiveDayForecast : ContentPage
{
    public FiveDayForecast()
    {
        InitializeComponent();
        GetLocationDataUsingPostalCode();
    }

    private async void GetFiveDayForecast(double latitude, double longitude)
    {
        var url =
            $"http://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&appid=af814f7c81ec8ac0ad157b953140d72e&units=metric";
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var data = await response.Content.ReadAsStringAsync();
        var ForecastData = JsonSerializer.Deserialize<fiveDayForecast.RootObject>(data);
        var forecastList = new List<fiveDayForecast.List>(ForecastData.list);
        var forecastTimes = new List<string>();
        var forecastTemps = new List<double>();

        foreach (var forecast in forecastList)
            if (forecast.dt_txt.Contains("12:00:00"))
            {
                var forecastDate = DateTime.Parse(forecast.dt_txt);
                var day = forecastDate.DayOfWeek.ToString();
                forecastTimes.Add(day);
                forecastTemps.Add(forecast.main.temp);
            }

        var forecastData = new List<ForecastData>();
        for (var i = 0; i < forecastTimes.Count; i++)
            forecastData.Add(new ForecastData
            {
                Day = forecastTimes[i],
                Temp = forecastTemps[i],
                Icon = forecastList[i].weather[0].icon
            });

        forecastListView.ItemsSource = forecastData;
    }

    private async void GetLocationDataUsingPostalCode()
    {
        var url =
            "https://api.openweathermap.org/geo/1.0/zip?zip=L6L2H4,CA&appid=af814f7c81ec8ac0ad157b953140d72e";
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var data = await response.Content.ReadAsStringAsync();
        var locationData = JsonSerializer.Deserialize<Geocoding.RootObject>(data);
        GetFiveDayForecast(locationData.lat, locationData.lon);
        // CityEntry.Text = locationData.name;
    }
}

internal class ForecastData
{
    public string Day { get; set; }
    public double Temp { get; set; }
    public string Icon { get; set; }
}