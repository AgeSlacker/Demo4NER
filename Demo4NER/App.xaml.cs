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

        public Command SendRefreshCommand { get; set; }

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
            SendRefreshCommand = new Command(SendRefreshToAllPages);
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
                        .Include(b=>b.Reviews)
                            .ThenInclude(r=>r.User) 
                        .Include(b=>b.Category)
                        .ToListAsync();// TODO make this safer
                    Debug.WriteLine("Loaded to cache");

                    CachedCategories = await db.Categories.ToListAsync();
                }
            });
            semaphore.Release();
        }

        protected override void OnSleep()
        {

            SavePropertiesAsync();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void SendRefreshToAllPages()
        {
            MessagingCenter.Send<App>(this,"refresh");
        }

        public async Task<Location> GetLocationAsync(bool forcenew = false)
        {
            Location location = null;
            if (LocationGranted)
            {
                try
                {
                    if (!forcenew)
                    {
                        Location cachedLocation = await Geolocation.GetLastKnownLocationAsync();
                        TimeSpan diff;
                        if (cachedLocation != null)
                        {
                            diff = DateTimeOffset.Now.Subtract(cachedLocation.Timestamp);
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

        public enum LocationProperties
        {
            On, NotEnabled, NotSupported, PermissionError
        }

    }
}
