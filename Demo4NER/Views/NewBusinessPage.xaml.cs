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
    public partial class NewBusinessPage : ContentPage
    {
        AddNewBusinessViewModel viewModel;
        public NewBusinessPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new AddNewBusinessViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadBusinessListCommand.Execute(null);
            //await viewModel.LoadBusinesses();
        }
    }

}
