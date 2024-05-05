using CommonWeatherApp.Models;
using CommonWeatherApp.Services;

namespace CommonWeatherApp;

public partial class WeatherPage : ContentPage
{
    public List<Models.List> WeatherList;
    public double lat;
    public double lon;
	public WeatherPage()
	{
		InitializeComponent();
        WeatherList = new List<Models.List>();
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocationAsync();
        await GetWeatherDataByLocation();

    }

    private async Task GetWeatherDataByLocation()
    {
        var result = await ApiService.GetWeatherAsync(lat, lon);
        ConfigureWeatherDatas(result);

    }
    private async Task GetWeatherDataByCity(string city)
    {
        var result = await ApiService.GetWeatherByCityAsync(city);
        ConfigureWeatherDatas(result);
    }

    private void ConfigureWeatherDatas(Root result)
    {
        WeatherList.Clear();
        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }
        CvWeather.ItemsSource = WeatherList;
        LblCity.Text = result.city.name;

        LblWeatherDescription.Text = result.list.First().weather.First().description;

        LblTemperature.Text = result.list.First().main.FormatedTemp + "°C";
        LblHumidity.Text = result.list.First().main.humidity + "%";
        LblWind.Text = result.list.First().wind.speed + "km/h";

        ImgWeatherIcon.Source = result.list.First().weather.First().CustomIconUrl;
    }

    public async Task GetLocationAsync()
    {
        Location location = await Geolocation.GetLocationAsync();

        lat = location.Latitude;
        lon = location.Longitude;
    }
    private async void TapLocation_Tapped(object sender, EventArgs e)
    {
        await GetLocationAsync();
        await GetWeatherDataByLocation();
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var response = await DisplayPromptAsync("", "", placeholder: "Search weather by city", accept: "Search", cancel: "Cancel");
            if (response != null)
            {
                await GetWeatherDataByCity(response);
            }
        }catch(Exception ex)
        {
            await DisplayAlert("Sorry", "Could not fetch the location for provided city", "OK");

        }

    }
}