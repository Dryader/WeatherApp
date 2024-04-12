﻿using System.Text.Json;

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
        Windd.Text = weatherData.wind.speed.ToString();
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

    private class LocationData
    {
        public string zip { get; set; }
        public string name { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
    }

    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public int sea_level { get; set; }
        public int grnd_level { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
        public double gust { get; set; }
    }

    public class Rain
    {
        public double _1h { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Root
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Rain rain { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }
}