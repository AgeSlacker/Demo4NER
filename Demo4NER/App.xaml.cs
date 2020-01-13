using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.Views;
using Demo4NER.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo4NER.Services;
using Demo4NER.Views;
using Java.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;

namespace Demo4NER
{
    public partial class App : Application
    {
        public ProfilePage ProfilePage { get; set; }
        public MainPage MainAppPage { get; set; }
        public SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        public bool FirstTime { get; set; } = true;
        public ICollection<Business> CachedBusinesses { get; set; } = null;
        public ICollection<Category> CachedCategories { get; set; } = null;
        public bool LocationEnabled { get; set; }
        public bool LocationGranted { get; set; } = false;

        private String syncfusionLicenceKey =
            "MTk0OTU1QDMxMzcyZTM0MmUzMGxKZDBKQ0lZZUdwSEd3NG1mRUV5MS8rQ0ZQWVdiRm55c2l4MlRTT2M0RE09";
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncfusionLicenceKey);
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            Debug.WriteLine(Properties.ToString());
        }

        protected override async void OnStart()
        {
            if (!Properties.ContainsKey("logged"))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainAppPage = new MainPage();
                MainPage = new NavigationPage(MainAppPage);
            }

            // Pre-load businesses
            await semaphore.WaitAsync();
            await Task.Run(async () =>
            {
                Debug.WriteLine("Started loading to cache");
                using (var db = new NerContext())
                {
                    CachedBusinesses = await db.Businesses
                        .Include(b => b.BusinessTags)
                            .ThenInclude(bt => bt.Tag)
                        .Include(b => b.Category)
                        .Include(b => b.Reviews)
                            .ThenInclude(r => r.User)
                        .ToListAsync();// TODO make this safer
                    Debug.WriteLine("Loaded to cache");

                    CachedCategories = await db.Categories.ToListAsync();
                }
            });
            semaphore.Release();

            await LocationUpdate(); // Try to load location beforehand
        }

        protected override void OnSleep()
        {

            SavePropertiesAsync();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public async Task<Location> GetLocationAsync(bool forcenew = false)
        {
            Location location = null;
            try
            {
                if (!forcenew)
                {
                    Location cachedLocation = await Geolocation.GetLastKnownLocationAsync();
                    if (cachedLocation != null)
                    {
                        TimeSpan diff = DateTimeOffset.Now.Subtract(cachedLocation.Timestamp);
                        if (diff.Minutes > 1)
                            forcenew = true;
                        else
                            location = cachedLocation;
                    }
                    else forcenew = true;
                }
                if (forcenew)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Best);
                    location = await Geolocation.GetLocationAsync(request);
                }

                LocationEnabled = true;

            }
            catch (FeatureNotSupportedException fns)
            {
                Debug.WriteLine(fns);
            }
            catch (FeatureNotEnabledException fne)
            {
                LocationEnabled = false;
                Debug.WriteLine(fne);
            }
            catch (PermissionException pe)
            {
                Debug.WriteLine(pe);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return location;
        }

        public void SaveUserInProperties(User user)
        {
            String serialized = JsonConvert.SerializeObject(user);
            if (Properties.ContainsKey("logged"))
                Properties["logged"] = serialized;
            else
                Properties.Add("logged", serialized);
        }

        public User GetUserFromProperties()
        {
            User user = null;
            if (Properties.ContainsKey("logged"))
                user = JsonConvert.DeserializeObject<User>((string)Properties["logged"]);
            return user;
        }

        public void RemoveUserFromProperties()
        {
            if (Properties.ContainsKey("logged"))
            {
                Properties.Remove("logged");
            }
        }

        public async Task CheckLocationPermissionsFromPage(Page page)
        {
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (status != PermissionStatus.Granted)
            {
                // ask for permission
                //if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                //{
                bool requestPermission = await page.DisplayAlert("Hot babes in your area", "They want to know your location", "Of course!",
                    "Maybe another time");
                //}
                if (requestPermission)
                {
                    var permissionStatuses = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = permissionStatuses[Permission.Location];
                }
            }

            if (status == PermissionStatus.Granted)
            {
                await LocationUpdate();
            }
            else if (status == PermissionStatus.Disabled)
            {
                await page.DisplayAlert("Oh no", "Enable location in your phone setting", "Sry im dumb");
            }
            else if (status != PermissionStatus.Unknown)
            {
                // denied

            }
        }

        private async Task LocationUpdate()
        {
            if (CachedBusinesses != null)
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted) return;
                var userLocation = await GetLocationAsync();
                if (userLocation == null) return;
                foreach (Business business in CachedBusinesses)
                {
                    business.Distance = Location.CalculateDistance(business.Location, userLocation,
                        DistanceUnits.Kilometers);
                }
                MessagingCenter.Send<App>(this, "locationUpdate");
            }
        }

        public enum LocationProperties
        {
            On, NotEnabled, NotSupported, PermissionError
        }

    }
}
