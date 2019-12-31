﻿using System;
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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public string SearchTerms { get; set; }
        public ObservableCollection<Business> BusinessesList { get; set; } = new ObservableCollection<Business>();

        public Command UpdateBusinessesListCommand { get; set; }
        public Command DoSearchCommand { get; set; }
        public SearchViewModel()
        {
            UpdateBusinessesListCommand = new Command(async () => await UpdateBusinessesListExecute());
            DoSearchCommand = new Command(async ()=> await DoSearchExecute());
        }

        private async Task DoSearchExecute()
        {
            BusinessesList.Clear();
            var temp = await Task.Run(()=>GetBusinessByName(SearchTerms));
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
                var result = await Task.Run(async ()=> 
                {
                    using (var db = new NerContext())
                    {
                        return await db.Businesses.ToListAsync();
                    }
                });
                BusinessesList.Clear();
                Location userLocation = await ((App) Application.Current).GetLocationAsync();
                foreach (Business business in result)
                {
                    business.Distance =
                        Location.CalculateDistance(business.Location, userLocation, DistanceUnits.Kilometers);
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
    }
}
