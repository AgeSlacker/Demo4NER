using Demo4NER.Models;
using Demo4NER.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class AddNewBusinessViewModel : BaseViewModel
    {
        public Business NewBusiness { get; set; } = new Business();
        public Command AddNewBusinessCommand { get; set; }
        public ObservableCollection<Business> Businesses { get; set; } = new ObservableCollection<Business>();
        public Command RefreshBusinessListCommand { get; set; }
        public Command LoadBusinessListCommand { get; set; }

        NerContext db = new NerContext();
        public AddNewBusinessViewModel()
        {
            AddNewBusinessCommand = new Command(() =>
            {
                using (var db = new NerContext())
                {
                    db.Add(NewBusiness);
                    db.SaveChanges();
                }
            });
            RefreshBusinessListCommand = new Command(async () => await LoadBusinesses());
            LoadBusinessListCommand = new Command(async () => await LoadBusinesses());
        }

        private async Task LoadBusinesses()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Debug.WriteLine("Inside");
                Businesses.Clear();
                var businessesFromDb = await Task.Run(()=>GetBusinessesAsync());

                foreach (var business in businessesFromDb)
                {
                    Businesses.Add(business);
                }
                Debug.WriteLine("Added to list");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            Debug.WriteLine("End");
        }

        private async Task<List<Business>> GetBusinessesAsync()
        {
            using (var db = new NerContext())
            {
                Debug.WriteLine("COntext created");
                var _bus = await db.Businesses.ToListAsync();
                Debug.WriteLine("ToLustAsync gotten");
                return _bus;
            }
        }
    }
}
