using CommonWeatherApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonWeatherApp.Services
{
    public class ApiService
    {
        public static async Task<Root> GetWeatherAsync(double lat, double lon)
        {
            var apiKey = "d0a720705340d6cc6aa7330e899aa8f5";
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&units=metric&appid={apiKey}");
            return JsonConvert.DeserializeObject<Root>(response);
        }
        public static async Task<Root> GetWeatherByCityAsync(string city)
        {
            var apiKey = "d0a720705340d6cc6aa7330e899aa8f5";
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/forecast?q={city}&units=metric&appid={apiKey}");
            
            return JsonConvert.DeserializeObject<Root>(response);
        }
    }
}
