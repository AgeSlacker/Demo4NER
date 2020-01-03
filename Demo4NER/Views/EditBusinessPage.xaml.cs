using Demo4NER.Models;
using Demo4NER.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditBusinessPage : ContentPage
    {
        public EditBusinessPageViewModel viewModel;
        public EditBusinessPage(Business selectedBusiness)
        {
            InitializeComponent();
            BindingContext = viewModel = new EditBusinessPageViewModel(selectedBusiness);
        }

        private void Save(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Back(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}