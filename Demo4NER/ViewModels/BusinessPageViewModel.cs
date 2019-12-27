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
            //LoadBusinessCommand = new Command(async () => await LoadBusinessCommandExecute());
            business.Name = "Fixepizza";
            business.Description = "Pizzaria";
            business.Contact = "961234567";
            business.Email = "fixepizza@gmail.com";
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
