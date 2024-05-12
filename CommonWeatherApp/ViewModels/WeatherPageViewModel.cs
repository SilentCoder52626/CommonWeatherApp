using CommonWeatherApp.Models;
using CommonWeatherApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CommonWeatherApp.ViewModels
{
    public partial class WeatherPageViewModel : ObservableObject
    {
        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        ObservableCollection<FutureWeatherModel> futureWeathers;

        public double lat;
        public double lon;

        private readonly IApiService _apiService;
        public WeatherPageViewModel(IApiService apiService)
        {
            FutureWeathers = new ObservableCollection<FutureWeatherModel>();
            _apiService = apiService;
            GetCurrentLocationAsync();
        }
        [ObservableProperty]
        string city;
        [ObservableProperty]
        string description;


        [ObservableProperty]
        string weatherIcon;
        [ObservableProperty]
        string humidity;
        [ObservableProperty]
        string temperature;
        [ObservableProperty]
        string wind;


        private async Task GetWeatherDataByLocation()
        {
            if(IsBusy) return;
            try
            {
                IsBusy = true;
                var result = await _apiService.GetWeatherAsync(lat, lon);
                ConfigureWeatherDatas(result);
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Sorry", $"Could not fetch the data : {ex.Message}", "OK");

            }
            finally
            {
                IsBusy = false;
            }
            

        }
        private async Task GetWeatherDataByCity(string city)
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                var result = await _apiService.GetWeatherByCityAsync(city);
                ConfigureWeatherDatas(result);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                IsBusy = false;
            }
            
        }

        private void ConfigureWeatherDatas(Root result)
        {
            FutureWeathers.Clear();

            foreach (var item in result.list)
            {
                FutureWeathers.Add(new FutureWeatherModel()
                {
                    CustomIconUrl = item.weather[0].CustomIconUrl,
                    FormatedTemp = item.main.FormatedTemp,
                    FTime = item.dateTime
                });
            }
            City = result.city.name;

            Description= result.list.First().weather.First().description;

            Temperature= result.list.First().main.FormatedTemp + "°C";
            Humidity= result.list.First().main.humidity + "%";
            Wind= result.list.First().wind.speed + "km/h";

            WeatherIcon = result.list.First().weather.First().CustomIconUrl;
        }

        public async Task GetLocationAsync()
        {
            Location location = await Geolocation.GetLocationAsync();

            lat = location.Latitude;
            lon = location.Longitude;
        }
        [RelayCommand]
        async Task GetCurrentLocationAsync()
        {
            await GetLocationAsync();
            await GetWeatherDataByLocation();
        }
        [RelayCommand]
        async Task SearchByCityAsync()
        {
            try
            {
                

                var response = await App.Current.MainPage.DisplayPromptAsync("", "", placeholder: "Search weather by city", accept: "Search", cancel: "Cancel");
                if (response != null)
                {
                    await GetWeatherDataByCity(response);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Sorry", $"Could not fetch the location for provided city", "OK");

            }

        }



    }
    public class FutureWeatherModel 
    {
        public double FormatedTemp { get; set; }
        public string CustomIconUrl { get; set; }
        public string FTime { get; set; }
    }
}
