using Demo4NER.Models;
using Demo4NER.ViewModels;
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
    public partial class EditProfilePage : ContentPage
    {
        public EditProfileViewModel viewModel;
       
        public EditProfilePage(User MyUser)
        {
            InitializeComponent();
            BindingContext = viewModel = new EditProfileViewModel(MyUser);
        }
        private void Save_Clicked(object sender, EventArgs e)
        {
            //guardar os inputs
            Navigation.PopAsync();
        }
    }
}