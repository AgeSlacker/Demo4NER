using Demo4NER.Models;
using Demo4NER.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class EditBusinessPageViewModel : BaseViewModel
    {
        private Business _business;

        public Business Business
        {
            get => _business;
            set => SetProperty(ref _business, value);
        }
        public Command LoadBusinessCommand { get; set; }

        public EditBusinessPageViewModel(Business selectedBusiness)
        {
            Business = selectedBusiness;
        }
    }
}
