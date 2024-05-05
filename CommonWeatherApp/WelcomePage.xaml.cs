namespace CommonWeatherApp;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
	}

    private async void BtnGetStarted_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushModalAsync(new WeatherPage());
    }
}