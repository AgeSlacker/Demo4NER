using Demo4NER.Models;
using Demo4NER.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class BusinessPageViewModel:BaseViewModel
    {

        public Business business { get; set; } = new Business();

        public int businessID = 1;

        public Command LoadBusinessCommand { get; set; }

        public BusinessPageViewModel()
        {
            LoadBusinessCommand = new Command(async () => await LoadBusinessCommandExecute());
        }

        private async Task LoadBusinessCommandExecute()
        {
            var auxBusiness = await GetBusinessAsync();
            business = auxBusiness;
        }

        private async Task<Business> GetBusinessAsync()
        {
            using (var db = new NerContext())
            {
                return await db.Businesses.FindAsync(businessID);
            }
        }
    }
}
