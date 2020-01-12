using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Demo4NER.ViewModels;
using Xamarin.Forms;

namespace Demo4NER.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class RegisterPage : ContentPage
    {
        private RegisterViewModel viewModel;
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new RegisterViewModel();
            viewModel.RegisterSuccessful += ViewModel_RegisterSuccessful;
        }

        private async void ViewModel_RegisterSuccessful(object sender, EventArgs e)
        {
            SuccessLabel.IsVisible = true;
            await Task.Delay(1000);
            await Navigation.PopAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Navigation.PopAsync();
        }

        private async void BackToLoginTap(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}
