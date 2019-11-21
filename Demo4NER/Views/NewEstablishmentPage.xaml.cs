using Demo4NER.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewEstablishmentPage : ContentPage
    {
        public NewEstablishmentViewModel viewModel;
        public NewEstablishmentPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new NewEstablishmentViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadListCommand.Execute(null);
        }
    }
}