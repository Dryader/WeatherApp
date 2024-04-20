using System.Text.Json;
using WeatherApp.BusinessLogic;

namespace WeatherApp.Pages;

public partial class MainPage : ContentPage
{
    private JsonManager _manager;
    private string _lastCode;
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public MainPage(JsonManager manager) : this()
    {
        _manager = manager;

        List<User> users = _manager.GetUsers();

        foreach (User user in users)
        {
            if (user.Username == _manager.CurrentUser)
            {
                if (user.PostalCode != null)
                {
                    CityEntry.Text = user.PostalCode;
                    GetLocationDataUsingPostalCode();
                }
            }
        }
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
        Description.Text = weatherData.weather[0].description;
        Humidity.Text = "Humidity: " + weatherData.main.humidity + "%";
        WindLocal.Text = "Wind: " + weatherData.wind.speed + "km/h";
        FeelsLike.Text = "Feels Like: " + weatherData.main.feels_like + "°C";
        MinTemp.Text = "Min Temp: " + weatherData.main.temp_min + "°C";
        MaxTemp.Text = "Max Temp: " + weatherData.main.temp_max + "°C";
    }

    private async void GetLocationDataUsingPostalCode()
    {
        var url =
            $"https://api.openweathermap.org/geo/1.0/zip?zip={CityEntry.Text},CA&appid=af814f7c81ec8ac0ad157b953140d72e";
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var data = await response.Content.ReadAsStringAsync();
        var locationData = JsonSerializer.Deserialize<GeocodingLogic.RootObject>(data);
        //check if postal code is valid
        if (locationData.lat == 0 && locationData.lon == 0)
        {
            CityEntry.Text = "Invalid Postal Code";
            return;
        }

        GetWeatherData(locationData.lat, locationData.lon);
        _lastCode = CityEntry.Text;
        CityEntry.Text = locationData.name;
    }

    private void ShowMap_OnClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new MapPage());
    }

    async private void SetFavoriteButton_Clicked(object sender, EventArgs e)
    {
        List<User> users = _manager.GetUsers();
        if (CityEntry.Text != null)
        {
            foreach (User user in users)
            {
                if (user.Username == _manager.CurrentUser)
                {
                    var url =
            $"https://api.openweathermap.org/geo/1.0/zip?zip={CityEntry.Text},CA&appid=af814f7c81ec8ac0ad157b953140d72e";
                    var client = new HttpClient();
                    var response = await client.GetAsync(url);
                    var data = await response.Content.ReadAsStringAsync();
                    var locationData = JsonSerializer.Deserialize<GeocodingLogic.RootObject>(data);
                    //check if postal code is valid
                    if (locationData.lat == 0 && locationData.lon == 0)
                    {
                        user.PostalCode = _lastCode;
                    }
                    user.PostalCode = CityEntry.Text;
                    _manager.SetUsers(users);
                }
            }
        }
    }

    private void FavoriteButton_Clicked(object sender, EventArgs e)
    {
        List<User> users = _manager.GetUsers();

        foreach (User user in users)
        {
            if (user.Username == _manager.CurrentUser)
            {
                if (user.PostalCode != null)
                {
                    CityEntry.Text = user.PostalCode;
                    GetLocationDataUsingPostalCode();
                }
            }
        }
    }
}