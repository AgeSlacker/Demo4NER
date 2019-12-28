using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo4NER.Services;
using Demo4NER.Views;
using Xamarin.Essentials;

namespace Demo4NER
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public async Task<Location> GetLocationAsync()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);

            Location location = await Geolocation.GetLocationAsync(request);

            if (Current.Properties.ContainsKey("UserLocation"))
                Current.Properties["UserLocation"] = location;
            else
                Current.Properties.Add("UserLocation", location);

            return location;
        }
    }
}
