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
    public partial class SearchControlPage : ContentPage
    {
        public SearchControlPage(ViewModels.SearchViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}