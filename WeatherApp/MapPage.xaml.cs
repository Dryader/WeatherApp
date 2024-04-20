namespace WeatherApp;

public partial class MapPage : ContentPage
{
    public MapPage()
    {
        InitializeComponent();

        var url =
            "https://embed.windy.com/embed.html?type=map&location=coordinates&metricRain=default&metricTemp=default&metricWind=default&zoom=6&overlay=wind&product=ecmwf&level=surface&lat=44.419&lon=-79.445";
        webView.Source = url;
    }
}