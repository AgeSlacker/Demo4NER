using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo4NER.Models;
using Demo4NER.Services;
using Microsoft.EntityFrameworkCore;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class BaseBusinessListViewModel : BaseViewModel
    {
        public string SearchTerms { get; set; }
        public ObservableCollection<Business> BusinessesList { get; set; } = new ObservableCollection<Business>();

        public Command UpdateBusinessesListCommand { get; set; }
        public Command DoSearchCommand { get; set; }
        private bool _displayLocationError = false;
        public bool DisplayLocationError
        {
            get => _displayLocationError;
            set => SetProperty(ref _displayLocationError, value);
        }
        public BaseBusinessListViewModel()
        {
            UpdateBusinessesListCommand = new Command(async () => await UpdateBusinessesListExecute());
            DoSearchCommand = new Command(async () => await DoSearchExecute());
        }

        private async Task DoSearchExecute()
        {
            BusinessesList.Clear();
            var temp = await Task.Run(() => GetBusinessByName(SearchTerms));
            foreach (Business business in temp)
            {
                BusinessesList.Add(business);
            }
        }

        private async Task<List<Business>> GetBusinessByName(String name)
        {
            using (var db = new NerContext())
            {
                return await db.Businesses.Where(b => b.Name.ToLower().Contains(name.ToLower())).ToListAsync();
            }
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

        public virtual async Task<List<Business>> GetBusinesses()
        {
            using (var db = new NerContext())
            {
                return await db.Businesses.ToListAsync();
            }
        }
    }
}
