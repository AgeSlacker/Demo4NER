﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Demo4NER.ViewModels
{
    public class BaseBusinessListViewModel : BaseViewModel
    {
        private static ObservableCollection<Tag> _tags = new ObservableCollection<Tag>();
        public static ObservableCollection<Tag> Tags => _tags;

        public static bool TagsSet { get; set; }
        public string SearchTerms { get; set; }
        public ObservableCollection<Business> BusinessesList { get; set; } = new ObservableCollection<Business>();
        public List<int> SelectedFilterIndexes { get; set; } = new List<int>();
        public Command UpdateBusinessesListCommand { get; set; }
        public Command DoSearchCommand { get; set; }
        public static Command InitializeTagsCommand { get; set; }
        public static Command UpdateTagsCommand { get; set; }

        public Command ApplyFilterCommand { get; set; }

        private bool _displayLocationError = false;
        private bool _isListEmpty;

        public bool DisplayLocationError
        {
            get => _displayLocationError;
            set => SetProperty(ref _displayLocationError, value);
        }

        public bool IsListEmpty
        {
            get => _isListEmpty;
            set => SetProperty(ref _isListEmpty, value);
        }

        public Category SelectedCategory { get; set; }

        public BaseBusinessListViewModel()
        {
            UpdateBusinessesListCommand = new Command(async () => await UpdateBusinessesListExecute());
            //DoSearchCommand = new Command(async () => await DoSearchExecute());
            InitializeTagsCommand = new Command(async () =>
            {
                // Initialize tags
                if (TagsSet) return;
                TagsSet = true;
                await Task.Run(() =>
                {
                    ICollection<Tag> result = null;
                    using (var db = new NerContext())
                    {
                        result = db.Tags.ToList();
                    }
                    foreach (Tag tag in result) _tags.Add(tag);
                });
            });
            UpdateTagsCommand = new Command(async () =>
            {
                _tags = new ObservableCollection<Tag>(); // TODO Why .Clear() does not work?
                await Task.Run(() =>
                {
                    ICollection<Tag> result = null;

                    using (var db = new NerContext())
                    {
                        result = db.Tags.ToList();
                    }

                    foreach (Tag tag in result) _tags.Add(tag);
                });
            });
            ApplyFilterCommand = new Command(async () =>
            {
                await UpdateBusinessesListExecute();
                if (SelectedCategory != null)
                {
                    var temp = new List<Business>(BusinessesList.Where(b => b.Category == SelectedCategory));
                    BusinessesList.Clear();
                    foreach (Business business in temp)
                    {
                        BusinessesList.Add(business);
                    }
                }

                if (!string.IsNullOrEmpty(SearchTerms))
                {
                    var tokenized = SearchTerms.Split(' ');
                    //var temp = new List<Business>(BusinessesList.Where(
                    //    b => tokenized.Any(t => t.Contains(b.Name))));
                    var temp = new List<Business>();
                    foreach (string s in tokenized)
                    {
                        // Match by name
                        foreach (Business business in BusinessesList)
                        {
                            if (business.Name.ToLower().Contains(s.ToLower()) && !temp.Contains(business))
                            {
                                temp.Add(business);
                            }
                        }
                        // Match by tag 
                        foreach (Business business in BusinessesList)
                        {
                            foreach (BusinessTag businessTag in business.BusinessTags)
                            {
                                var val = businessTag.Tag.Value;
                                if (val.ToLower().Contains(s.ToLower()) && !temp.Contains(business))
                                {
                                    temp.Add(business);
                                }
                            }
                        }
                    }

                    BusinessesList.Clear();
                    foreach (Business business in temp)
                    {
                        BusinessesList.Add(business);
                    }

                    SelectedCategory = null;
                    SearchTerms = null;
                }
                IsListEmpty = !BusinessesList.Any();
            });
            MessagingCenter.Subscribe<App>(this, "refresh", async (app) => { await UpdateBusinessesListExecute(); });
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

        public virtual async Task UpdateBusinessesListExecute()
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

                BusinessesList.Clear();
                PermissionStatus status =
                    await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status == PermissionStatus.Granted)
                {
                    Location userLocation = await ((App)Application.Current).GetLocationAsync();
                    if (((App)Application.Current).LocationEnabled)
                    {
                        foreach (Business business in ((App)Application.Current).CachedBusinesses)
                            business.Distance = Location.CalculateDistance(business.Location, userLocation,
                                DistanceUnits.Kilometers);
                        DisplayLocationError = false;
                    }
                    else
                    {
                        DisplayLocationError = true;
                    }
                }
                else
                {
                    DisplayLocationError = true;
                }

                foreach (Business business in ((App)Application.Current).CachedBusinesses)
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
                IsListEmpty = !BusinessesList.Any();
                IsBusy = false;
            }
        }

        public virtual async Task<List<Business>> GetBusinesses()
        {
            using (var db = new NerContext())
            {
                return await db.Businesses
                    .Include(b => b.BusinessTags)
                    .ThenInclude(bt => bt.Tag)
                    .ToListAsync();
            }
        }

    }
}
