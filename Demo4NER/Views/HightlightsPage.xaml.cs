using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.Content.PM;
using Android.Webkit;
using Demo4NER.ViewModels;
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
        public HighlightsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new HighlightsViewModel();
        }

        protected override async void OnAppearing()
        {
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (status != PermissionStatus.Granted)
            {
                // ask for permission
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                {
                    await DisplayAlert("Hot babes in your area", "They want to know your location", "Of course!",
                        "Maybe another time");
                }

                var permissionStatuses= await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                status = permissionStatuses[Permission.Location];
            }

            if (status == PermissionStatus.Granted)
            {
                // grated

            } else if (status == PermissionStatus.Disabled)
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
    }
}