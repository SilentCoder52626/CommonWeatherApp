namespace CommonWeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            VersionTracking.Track();
            if (VersionTracking.IsFirstLaunchEver)
            {
                MainPage = new WelcomePage();
            }
            else
            {
                MainPage = new WeatherPage();
            }

        }
    }
}
