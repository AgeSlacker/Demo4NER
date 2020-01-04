﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Threading.Tasks;
using Demo4NER.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo4NER.Services;
using Demo4NER.Views;
using Java.Util;
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

        public bool LocationEnabled { get; set; }
        public bool LocationGranted { get; set; } = false;

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            Debug.WriteLine(Properties.ToString());
        }

        protected override void OnStart()
        {
            if (!Properties.ContainsKey("logged"))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
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

        public enum LocationProperties
        {
            On, NotEnabled, NotSupported, PermissionError
        }

    }
}
