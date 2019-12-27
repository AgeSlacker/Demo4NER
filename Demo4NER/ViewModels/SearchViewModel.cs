using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Demo4NER.Models;
using Demo4NER.Services;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    class SearchViewModel : BaseViewModel
    {
        public ObservableCollection<Business> BusinessesList { get; set; }

        public Command UpdateBusinessesListCommand { get; set; }

        public SearchViewModel()
        {
            UpdateBusinessesListCommand = new Command(async () => await UpdateBusinessesListExecute());
            UpdateBusinessesListExecute();
        }

        private async Task UpdateBusinessesListExecute()
        {
            var _result = await Task.Run(() =>
            {
                using (var db = new NerContext())
                {
                    return db.Businesses.ToListAsync();
                }
            });
            foreach (Business business in _result)
            {
                BusinessesList.Add(business);
            }
        }
    }
}
