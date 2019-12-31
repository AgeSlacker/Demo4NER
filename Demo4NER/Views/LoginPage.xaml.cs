using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Demo4NER.Models;
using Demo4NER.ViewModels;
using Xamarin.Forms;

namespace Demo4NER.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel viewModel;
        
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new LoginViewModel();
            viewModel.LoginAttempted += ViewModel_LoginAttempted;
        }

        private async void ViewModel_LoginAttempted(object sender, LoginViewModel.LoginResult e)
        {

            (Application.Current as App).SaveUserInProperties(viewModel.User);
            if (Navigation.ModalStack.Contains(this.Parent))
                await Navigation.PopModalAsync();
            else
                await Navigation.PopAsync();
        }


        private async void RegisterClickGesture(object sender, EventArgs e)
        {
            viewModel.Error = null;
            await Navigation.PushAsync(new RegisterPage());
        }

    }
}
