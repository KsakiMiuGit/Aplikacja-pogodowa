namespace WeatherApp
{
    public class GPS
    {
        public async Task GetLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync
                    (
                    new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Best,
                        Timeout = TimeSpan.FromSeconds(30),
                        RequestFullAccuracy = true
                    }

                    );
            }
            if (location == null)
            {
                await Shell.Current.DisplayAlert("Error", "We couldn't get your location", "OK");
                return;
            }
        }

    }
}
