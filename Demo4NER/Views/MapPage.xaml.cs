using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo4NER.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private MapViewModel viewModel;
        public MapPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MapViewModel();
        }

        protected override async void OnAppearing()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);

            Location location = await Geolocation.GetLocationAsync(request);

            Debug.WriteLine(location.Accuracy + " " + location.Latitude + " " + location.Longitude);
            ((StackLayout)this.Content).Children.Add(
                new Label()
                {
                    Text = location.Accuracy + " " + location.Latitude + " " + location.Longitude
                });

            var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);

            var placemark = placemarks?.FirstOrDefault();
            if (placemark != null)
            {
                var geocodeAddress =
                    $"AdminArea:       {placemark.AdminArea}\n" +
                    $"CountryCode:     {placemark.CountryCode}\n" +
                    $"CountryName:     {placemark.CountryName}\n" +
                    $"FeatureName:     {placemark.FeatureName}\n" +
                    $"Locality:        {placemark.Locality}\n" +
                    $"PostalCode:      {placemark.PostalCode}\n" +
                    $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                    $"SubLocality:     {placemark.SubLocality}\n" +
                    $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                    $"Thoroughfare:    {placemark.Thoroughfare}\n";

                Console.WriteLine(geocodeAddress);
                (Content as StackLayout).Children.Add(new Label(){Text = geocodeAddress});
            }
        }
    }
}