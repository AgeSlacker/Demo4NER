using System;
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
    }
}