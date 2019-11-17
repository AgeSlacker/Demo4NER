using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo4NER.Models;
using Demo4NER.Views;
using Demo4NER.ViewModels;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        ProfileViewModel viewModel;
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ProfileViewModel();
        }
        private void Edit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditProfilePage());
        }

    }
}