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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class MyBusinessesViewModel : BaseViewModel
    {
        public ObservableCollection<Business> BusinessesList { get; set; } = new ObservableCollection<Business>();
        public Command UpdateBusinessesListCommand { get; set; }
        public Command FetchCommand { get; set; }

        public MyBusinessesViewModel()
        {
            UpdateBusinessesListCommand = new Command(async () => await UpdateBusinessesListExecute());
            //FetchCommand = new Command(async () => await CatchExecute());
            BusinessesList.Clear();
            
            var user = (Application.Current as App).GetUserFromProperties();
            List<Business> Businesses = user.Businesses;
            foreach(Business business in Businesses)
            {
                BusinessesList.Add(business);
            }
        }

        /*private async Task CatchExecute()
        {
            BusinessesList.Clear();
            var temp = await Task.Run(() => GetBusinessByID(ID));
            foreach (Business business in temp)
            {
                BusinessesList.Add(business);
            }
        }

        private async Task<List<Business>> GetBusinessByID(String id)
        {
            using (var db = new NerContext())
            {
                return await db.Businesses.Where(b => b.UserId.Contains(id)).ToListAsync();
            }
        }*/

        private async Task UpdateBusinessesListExecute()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var result = await Task.Run(async () =>
                {
                    using (var db = new NerContext())
                    {
                        return await db.Businesses.ToListAsync();
                    }
                });
                BusinessesList.Clear();
                foreach (Business business in result)
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
    }
}