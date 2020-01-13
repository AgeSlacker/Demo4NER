using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Demo4NER.Models;
using Demo4NER.Services;
using Microsoft.EntityFrameworkCore;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Demo4NER.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        private bool _displayLocationError;
        public ObservableCollection<Business> BusinessesList { get; set; } = new ObservableCollection<Business>();
        public Command UpdateBusinessesCommand { get; set; }

        public MapViewModel()
        {
            UpdateBusinessesCommand = new Command(async () => await UpdateBusinessesListExecute());
            MessagingCenter.Subscribe<App>(this, "refresh", async (app) => { await UpdateBusinessesListExecute(); });
        }

        private async Task UpdateBusinessesListExecute()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                // tentar ir buscar à cache primeiro
                SemaphoreSlim semaphore = (Application.Current as App).semaphore;
                await semaphore.WaitAsync();
                if (((App)Application.Current).CachedBusinesses == null)
                {
                    Debug.WriteLine("Getting from db");
                    // Get Businesses and set to cache, otherwise use cached
                    var businesses = await Task.Run(GetBusinesses);
                    ((App)Application.Current).CachedBusinesses = new List<Business>(businesses);
                }
                else
                {
                    Debug.WriteLine("Using cached");
                }
                semaphore.Release();
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status == PermissionStatus.Granted)
                {
                    Location userLocation = await ((App)Application.Current).GetLocationAsync();
                    if (((App)Application.Current).LocationEnabled)
                    {
                        foreach (Business business in ((App)Application.Current).CachedBusinesses)
                        {
                            business.Distance =
                                Location.CalculateDistance(business.Location, userLocation, DistanceUnits.Kilometers);
                        }
                        DisplayLocationError = false;
                    }
                }
                else
                    DisplayLocationError = true;

                // Needs to be in main, UI thread 
                await Device.InvokeOnMainThreadAsync(() =>
                {
                    BusinessesList.Clear();
                    foreach (Business business in (Application.Current as App).CachedBusinesses)
                    {
                        BusinessesList.Add(business);
                    }
                });
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public bool DisplayLocationError
        {
            get => _displayLocationError;
            set => SetProperty(ref _displayLocationError,value);
        }

        public virtual async Task<List<Business>> GetBusinesses()
        {
            using (var db = new NerContext())
            {
                return await db.Businesses.ToListAsync();
            }
        }
    }
}
