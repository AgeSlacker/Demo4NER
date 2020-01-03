using Demo4NER.Models;
using Demo4NER.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessPage : ContentPage
    {
        public BusinessPageViewModel viewModel;
        public BusinessPage(Business selectedBusiness)
        {
            InitializeComponent();
            BindingContext = viewModel = new BusinessPageViewModel(selectedBusiness);
        }
    }
}