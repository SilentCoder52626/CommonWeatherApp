using CommonWeatherApp.Services;

namespace CommonWeatherApp;

public partial class WelcomePage : ContentPage
{
	private readonly IApiService _apiService;
    public WelcomePage(IApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private async void BtnGetStarted_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushModalAsync(new WeatherPage(_apiService));
    }
}