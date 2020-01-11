using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo4NER.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo4NER.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminHubPage : ContentPage
    {
        private AdminViewModel viewModel;
        public AdminHubPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new AdminViewModel();
        }

        private void TagCategoriesOnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditTagsCategoriesPage(viewModel));
        }
    }
}