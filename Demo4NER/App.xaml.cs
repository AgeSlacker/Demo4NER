using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo4NER.Services;
using Demo4NER.Views;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Xamarin.Essentials;

namespace Demo4NER
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            if (!Properties.ContainsKey("logged"))
            {
                LoginPage loginPage = new LoginPage();
                MainPage = new NavigationPage(loginPage);
                MainPage.Navigation.InsertPageBefore(new MainPage(), loginPage);
            }
            else
            {
                MainPage = new MainPage();
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public async Task<Location> GetLocationAsync(bool forceNew = false)
        {

            Location cachedLocation = await Geolocation.GetLastKnownLocationAsync();
            Location location;
            TimeSpan diff = DateTimeOffset.Now.Subtract(cachedLocation.Timestamp);
            Debug.WriteLine(diff);
            if (forceNew || diff.Minutes > 1)
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                location = await Geolocation.GetLocationAsync(request);
            }
            else location = cachedLocation;


            if (Current.Properties.ContainsKey("UserLocation"))
                Current.Properties["UserLocation"] = location;
            else
                Current.Properties.Add("UserLocation", location);

            return location;
        }
    }
}
