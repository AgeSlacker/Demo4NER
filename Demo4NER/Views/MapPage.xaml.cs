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
        private Map map;
        public MapPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MapViewModel();

            // Create map
            map = new Map(MapSpan.FromCenterAndRadius(
                new Position(40.205085, -8.410108),
                Distance.FromKilometers(5)));
            map.SetBinding(Map.ItemsSourceProperty, "BusinessesList");
            map.ItemTemplate = (DataTemplate)Resources["MapItemTemplate"];
            MainLayout.Children.Add(map);
            viewModel.UpdateBusinessesCommand.Execute(null);
            MessagingCenter.Subscribe<BaseViewModel, Business>(this, "navigate", async (sender, bus) =>
               {
                   await Device.InvokeOnMainThreadAsync(() =>
                   {
                       // TODO set to map
                       Pin pin = GetPinFromBusiness(bus);
                       if (pin != null)
                           map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromKilometers(1)));
                   });
               });
        }
        // TODO change GPS error to global elemnt on all pages para n repetir
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
                        //map.MoveToRegion(
                        //    MapSpan.FromCenterAndRadius(
                        //        new Position(userPosition.Latitude, userPosition.Longitude),
                        //        Distance.FromKilometers(1)));
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

        private async void Pin_OnInfoWindowClicked(object sender, PinClickedEventArgs e)
        {
            // find Business by Position
            Business business = GetBusinessFromPin(sender as Pin);
            if (business != null)
                await Navigation.PushModalAsync(new BusinessPage(business));
        }

        private Business GetBusinessFromPin(Pin pin)
        {
            Position pos = pin.Position;
            return (Application.Current as App).CachedBusinesses.FirstOrDefault(b => b.Longitude == pos.Longitude && b.Latitude == pos.Latitude);
        }

        private Pin GetPinFromBusiness(Business business)
        {
            Pin pin = map.Pins.FirstOrDefault(p =>
                p.Position.Longitude == business.Longitude && p.Position.Latitude == business.Latitude);
            if (pin == null)
            {
                viewModel.UpdateBusinessesCommand.Execute(null);
                pin = map.Pins.FirstOrDefault(p =>
                    p.Position.Longitude == business.Longitude && p.Position.Latitude == business.Latitude);
            }

            return pin;
        }
    }
}