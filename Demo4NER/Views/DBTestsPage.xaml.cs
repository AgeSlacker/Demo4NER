using Demo4NER.Models;
using Demo4NER.Services;
using Demo4NER.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DBTestsPage : ContentPage
    {

        DBTestsViewModel viewModel;
        public DBTestsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new DBTestsViewModel();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadUsersCommand.Execute(null);
        }

    }
}