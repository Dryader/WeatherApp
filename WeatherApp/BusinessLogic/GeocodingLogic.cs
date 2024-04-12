namespace WeatherApp.BusinessLogic;

public class GeocodingLogic
{
    public class RootObject
    {
        public string zip { get; set; }
        public string name { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
    }
}