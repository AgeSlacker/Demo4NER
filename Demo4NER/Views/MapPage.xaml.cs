using System.Linq;
using Demo4NER.Models;
using Demo4NER.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private MapViewModel viewModel;
        private bool _firstTime = true;
        public MapPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MapViewModel();
            viewModel.UpdateBusinessesCommand.Execute(null);
        }

        protected override async void OnAppearing()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (status == PermissionStatus.Granted)
            {
                if (((App)Application.Current).LocationEnabled)
                {
                    if (_firstTime)
                    {
                        var userPosition = await (Application.Current as App).GetLocationAsync();
                        map.IsShowingUser = true;
                        map.MoveToRegion(
                            MapSpan.FromCenterAndRadius(
                                new Position(userPosition.Latitude, userPosition.Longitude),
                                Distance.FromKilometers(1)));
                        _firstTime = false;
                    }
                    viewModel.DisplayLocationError = false;
                    return;
                }
            }
            viewModel.DisplayLocationError = true;
            // Esta página precisa de localização
            //var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            //if (status == PermissionStatus.Granted)
            //{
            //    Location location = await ((App)Application.Current).GetLocationAsync();
            //    if (((App) Application.Current).LocationEnabled)
            //    {
            //        Debug.WriteLine(location.Accuracy + " " + location.Latitude + " " + location.Longitude);
            //        ((StackLayout)this.Content).Children.Add(
            //            new Label()
            //            {
            //                Text = location.Accuracy + " " + location.Latitude + " " + location.Longitude
            //            });

            //        var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);

            //        var placemark = placemarks?.FirstOrDefault();
            //        if (placemark != null)
            //    {
            //        var geocodeAddress =
            //            $"AdminArea:       {placemark.AdminArea}\n" +
            //            $"CountryCode:     {placemark.CountryCode}\n" +
            //            $"CountryName:     {placemark.CountryName}\n" +
            //            $"FeatureName:     {placemark.FeatureName}\n" +
            //            $"Locality:        {placemark.Locality}\n" +
            //            $"PostalCode:      {placemark.PostalCode}\n" +
            //            $"SubAdminArea:    {placemark.SubAdminArea}\n" +
            //            $"SubLocality:     {placemark.SubLocality}\n" +
            //            $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
            //            $"Thoroughfare:    {placemark.Thoroughfare}\n";

            //        Console.WriteLine(geocodeAddress);
            //        (Content as StackLayout).Children.Add(new Label() { Text = geocodeAddress });
            //    }

            //    }
            //}
        }

        private void Pin_OnMarkerClicked(object sender, PinClickedEventArgs e)
        {

        }

        private async void Pin_OnInfoWindowClicked(object sender, PinClickedEventArgs e)
        {
            // find Business by Position
            Position pos = (sender as Pin).Position;
            Business business = viewModel.BusinessesList.First(b => b.Longitude == pos.Longitude && b.Latitude == pos.Latitude);
            await Navigation.PushModalAsync(new BusinessPage(business));
        }
    }
}