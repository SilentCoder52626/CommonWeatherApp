using CommonWeatherApp.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonWeatherApp.Services
{
    public interface IApiService
    {
        Task<Root> GetWeatherAsync(double lat, double lon);
        Task<Root> GetWeatherByCityAsync(string city);
    }
    public class ApiService : IApiService
    {
        private readonly IConfiguration _configuration;
        string apiKey = "";
        public ApiService(IConfiguration configuration)
        {
            _configuration = configuration;
            apiKey = _configuration["Weather_Api_Key"];

        }

        public async Task<Root> GetWeatherAsync(double lat, double lon)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&units=metric&appid={apiKey}");
            return JsonConvert.DeserializeObject<Root>(response);
        }
        public async Task<Root> GetWeatherByCityAsync(string city)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/forecast?q={city}&units=metric&appid={apiKey}");
            
            return JsonConvert.DeserializeObject<Root>(response);
        }
    }
}
