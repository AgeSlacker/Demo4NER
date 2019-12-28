using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Demo4NER.Models;
using Demo4NER.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo4NER.ViewModels
{
    class NewBusinessViewModel : BaseViewModel
    {
        public Business NewBusiness { get; set; } = new Business();

        public Command CreateNewBusinessCommand { get; set; }
        public NewBusinessViewModel()
        {
            CreateNewBusinessCommand = new Command(async () => await CreateNewBusinessExecute());
        }

        private async Task CreateNewBusinessExecute()
        {
            Debug.WriteLine(NewBusiness.ToString());

            await Task.Run(() =>
            {
                using (var db = new NerContext())
                {
                    db.Businesses.Add(NewBusiness);
                    db.SaveChanges();
                }
            });

            NewBusiness = new Business();
        }
    }
}
