using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.Content.PM;
using Android.Webkit;
using Demo4NER.Models;
using Demo4NER.ViewModels;
using Demo4NER.Views.Admin;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Permission = Plugin.Permissions.Abstractions.Permission;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HighlightsPage : ContentPage
    {
        private HighlightsViewModel viewModel;
        private bool firstTime = true;
        public HighlightsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new HighlightsViewModel();
            viewModel.UpdateBusinessesListCommand.Execute(null);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (firstTime)
            {
                await CheckLocationPermissions();
                firstTime = false;
            }
        }

        private async Task CheckLocationPermissions()
        {
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (status != PermissionStatus.Granted)
            {
                // ask for permission
                //if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                //{
                bool requestPermission = await DisplayAlert("Permissões de localização", 
                    "O 4NERApp precisa de necessita de permissões de localização para funcionar.", 
                    "Permitir",
                    "Não permitir");
                //}
                if (requestPermission)
                {
                    var permissionStatuses = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = permissionStatuses[Permission.Location];
                }
            }

            if (status == PermissionStatus.Granted)
            {
                // grated
                ((App) Application.Current).LocationGranted = true;
                // TODO send event to update distance
            }
            else if (status == PermissionStatus.Disabled)
            {
                await DisplayAlert("Oh no", "Enable location in your phone setting", "Sry im dumb");
            }
            else if (status != PermissionStatus.Unknown)
            {
                // denied

            }
        }
        private async void ClearPropertiesDEBUG(object sender, EventArgs e)
        {
            (Application.Current as App).Properties.Clear();
            await Application.Current.SavePropertiesAsync();
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Business business = e.Item as Business;
            if (business != null)
                Navigation.PushModalAsync(new BusinessPage(business));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            Navigation.PushModalAsync(new SearchControlPage(viewModel));
            IsBusy = false;
        }

        private void AdminPageOnClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdminHubPage());
        }
    }
}