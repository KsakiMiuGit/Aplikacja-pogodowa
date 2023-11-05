using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp
{
    public class WeatherConfig
    {
        public static string OpenWeatherMapEndpoint = $"https://api.openweathermap.org/data/2.5/weather";
        public static string OpenWeatherMapApiKey = "7527fa0408a409c579298975c457d826"; 

        HttpClient _httpClient;
        public WeatherConfig() 
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherData> GetWeatherData(string query)
        {
            WeatherData weatherData = null;
            try
            {   
                var responce = await _httpClient.GetAsync(query);
                if(responce.IsSuccessStatusCode)
                {
                    var content = await responce.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content);
                }
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            return weatherData;
            
        }
      
    }
}

