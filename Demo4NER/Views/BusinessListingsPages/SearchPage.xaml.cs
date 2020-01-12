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
    public partial class SearchPage : ContentPage
    {
        private SearchViewModel viewModel;
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new SearchViewModel();
            viewModel.UpdateBusinessesListCommand.Execute(null);
        }

        //private void ItemList_OnItemTapped(object sender, SelectedItemChangedEventArgs e)
        //{
        //    Business selectedBusiness = e.SelectedItem as Business;
        //    if (selectedBusiness != null)
        //        Navigation.PushModalAsync(new BusinessPage(selectedBusiness));
        //}

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (IsBusy) return;
            IsBusy = true;
            await Navigation.PushModalAsync(new SearchControlPage(viewModel));
            IsBusy = false;
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Business business = e.Item as Business;
            if (business != null)
                Navigation.PushModalAsync(new BusinessPage(business));
        }
    }
}