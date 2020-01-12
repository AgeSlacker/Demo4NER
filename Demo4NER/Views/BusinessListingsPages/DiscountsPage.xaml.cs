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

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Business business = e.Item as Business;
            if (business != null)
                Navigation.PushModalAsync(new BusinessPage(business));
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (IsBusy)
                return;
            IsBusy = true;
            Navigation.PushModalAsync(new SearchControlPage(viewModel));
            IsBusy = false;
        }
    }
}