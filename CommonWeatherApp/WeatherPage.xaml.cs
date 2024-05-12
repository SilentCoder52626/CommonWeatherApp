using CommonWeatherApp.Models;
using CommonWeatherApp.ViewModels;

namespace CommonWeatherApp;

public partial class WeatherPage : ContentPage
{
    public WeatherPage()
    {
        InitializeComponent();

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        var weatherPageViewModel = Application.Current.Handler.MauiContext.Services.GetService<WeatherPageViewModel>();

        BindingContext = weatherPageViewModel;
    }


}