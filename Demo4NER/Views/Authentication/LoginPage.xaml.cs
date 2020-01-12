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
            App cur = (Application.Current as App);
            cur.SaveUserInProperties(viewModel.User);
            if (!cur.FirstTime)
            {
                var profileNavigationPage = new NavigationPage(new ProfilePage())
                {
                    IconImageSource = ImageSource.FromFile("avatar.png"),
                    Title = "Perfil"
                };
                (Application.Current as App).MainAppPage.Children[4] = profileNavigationPage;
            }
            else
            {
                cur.FirstTime = false;
                await Navigation.PushAsync(new FakeAssProgressBarPage());
                cur.MainAppPage = new MainPage();
                Navigation.InsertPageBefore((Application.Current as App).MainAppPage, this);
                Navigation.RemovePage(this);
                await Navigation.PopAsync();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void RegisterClickGesture(object sender, EventArgs e)
        {
            viewModel.Error = null;
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void AnonimusLogin(object sender, EventArgs e)
        {
            (Application.Current as App).FirstTime = false;
            await Navigation.PushAsync(new FakeAssProgressBarPage());
            (Application.Current as App).MainAppPage = new MainPage();
            Navigation.InsertPageBefore((Application.Current as App).MainAppPage, this);
            Navigation.RemovePage(this);
            await Navigation.PopAsync();
        }

        private void AboutLabelOnTap(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AboutPage());
        }
    }
}
