using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
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
        }

        private async Task UpdateBusinessesListExecute()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var businesses = await Task.Run(GetBusinesses);
                BusinessesList.Clear();
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status == PermissionStatus.Granted)
                {
                    Location userLocation = await ((App)Application.Current).GetLocationAsync();
                    if (((App)Application.Current).LocationEnabled)
                    {
                        foreach (Business business in businesses)
                        {
                            business.Distance =
                                Location.CalculateDistance(business.Location, userLocation, DistanceUnits.Kilometers);
                        }
                        DisplayLocationError = false;
                    }
                }
                else
                    DisplayLocationError = true;

                foreach (Business business in businesses)
                {
                    BusinessesList.Add(business);
                }
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
