using CommonWeatherApp.Services;

namespace CommonWeatherApp
{
    public partial class App : Application
    {
        public App(IApiService apiService)
        {
            InitializeComponent();

            VersionTracking.Track();
            if (VersionTracking.IsFirstLaunchEver)
            {
                MainPage = new WelcomePage(apiService);
            }
            else
            {
                MainPage = new WeatherPage(apiService);
            }
        }
    }
}
