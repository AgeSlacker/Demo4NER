using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo4NER.Models;
using Demo4NER.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class DiscountsPage : ContentPage
    {
        private DiscountsViewModel viewModel;
        public DiscountsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new DiscountsViewModel();
            viewModel.UpdateBusinessesListCommand.Execute(null);
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Business selectedBusiness = e.SelectedItem as Business;
            if (selectedBusiness != null)
                Navigation.PushModalAsync(new BusinessPage(selectedBusiness));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SearchControlPage(viewModel));
        }
    }
}