/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo4NER.ViewModels;

namespace Demo4NER.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ProfilePage : ContentPage
   {
       public ProfileViewModel viewModel;
       public ProfilePage()
       {
           InitializeComponent();
           BindingContext = viewModel = new ProfileViewModel();
       }

       protected override void OnAppearing()
       {
           base.OnAppearing();
           viewModel.LoadUserCommand.Execute(null);
       }

       private void Edit_Clicked(object sender, EventArgs e)
       {
           Navigation.PushAsync(new EditProfilePage());
       }

       private void Button_OnClicked(object sender, EventArgs e)
       {
           Navigation.PushModalAsync(new NewBusinessPage());
       }
   }
}*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo4NER.ViewModels;
using Demo4NER.Models;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfileViewModel viewModel;
        public List<Review> Reviews { get; set; } = new List<Review>();
        public Review RevA { get; set; } = new Review();
        public Review RevB { get; set; } = new Review();
        public Business BusinessA { get; set; } = new Business();
        public Business BusinessB { get; set; } = new Business();

        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ProfileViewModel((Application.Current as App).GetUserFromProperties());

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.User = (Application.Current as App).GetUserFromProperties();
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditProfilePage((Application.Current as App).GetUserFromProperties()));
        }

        private async void LogoutButtonClicked(object sender, EventArgs e)
        {
            (Application.Current as App).RemoveUserFromProperties();
            await Navigation.PushModalAsync(new FakeAssProgressBarPage(500,true));
            await Task.Delay(500);
            (Application.Current as App).FirstTime = true;
            (Application.Current as App).MainPage = new NavigationPage( new LoginPage());
        }

        private void AboutButtonOnClick(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AboutPage());
        }
    }
}