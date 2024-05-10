using CommonWeatherApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonWeatherApp.ViewModels
{
    public partial class WeatherPageViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<FutureWeatherModel> futureWeathers;

        public double lat;
        public double lon;

        private readonly IApiService _apiService;
        public WeatherPageViewModel()
        {
            FutureWeathers = new ObservableCollection<FutureWeatherModel>();
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


        

    }
    public partial class FutureWeatherModel : ObservableObject
    {
        [ObservableProperty]
        string formatedTemp;
        [ObservableProperty]
        string customIconUrl;
        [ObservableProperty]
        string fTime;
    }
}
