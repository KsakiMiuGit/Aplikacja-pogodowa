using Microsoft.VisualBasic;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {

        private GPS gps;
        private WeatherConfig weatherConfig;
        public MainPage()
        {
            InitializeComponent();
            gps = new GPS();
            weatherConfig = new WeatherConfig();
            UpdateLocationInfo();
        }
        private async void UpdateLocationInfo()
        {
            await gps.GetLocation();
            Location location = await Geolocation.GetLastKnownLocationAsync();
            if (location != null)
            {
                lblLatitude.Text = $"Latitude: {location.Latitude}";
                lblLongitude.Text = $"Longitude: {location.Longitude}";
            }
            else
            {
                lblLatitude.Text = "Location data not available";
                lblLongitude.Text = "Location data not available";
            }
        }

        private async void GetWeatherButton(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(cityName.Text))
            {
                WeatherData weatherData = await weatherConfig.GetWeatherData(GenerateRequestURL(WeatherConfig.OpenWeatherMapEndpoint));
                BindingContext=weatherData;
                WeatherLocation.Text = $"Weather in: {cityName.Text}";
            }
        }
        string GenerateRequestURL(string endPoint)
        {
            string requestUri = endPoint;
            requestUri += $"?q={cityName.Text}";
            requestUri += "&units=metric";
            requestUri += $"&APPID={WeatherConfig.OpenWeatherMapApiKey}";
            return requestUri;
        }
    }
}