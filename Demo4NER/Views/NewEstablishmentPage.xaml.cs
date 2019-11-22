using Demo4NER.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Debug.WriteLine("On Appearing before");
            await Task.Run(()=>viewModel.LoadListCommand.Execute(null));
            Debug.WriteLine("On appearing after");
        }
    }
}