using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            // Login Success
            ((App) Application.Current).SaveUserInProperties(viewModel.User);
            //Debug.WriteLine(viewModel.User.ToString());
            Navigation.InsertPageBefore(new MainPage(), this);
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

        private async void AnonimusLogin(object sender, EventArgs e)
        {
            this.viewModel.IsBusy = true;
            await Task.Run(()=> Navigation.InsertPageBefore(new MainPage(), this));
            this.viewModel.IsBusy = false;
            if (Navigation.ModalStack.Contains(this.Parent))
                await Navigation.PopModalAsync();
            else
                await Navigation.PopAsync();
        }
    }
}