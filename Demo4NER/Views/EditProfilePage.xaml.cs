using Demo4NER.Models;
using Demo4NER.ViewModels;
using System;

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
        private void Button_OnClicked(object sender, EventArgs e)
        {
            viewModel.EditProfileCommand.Execute(null);
            Navigation.PopAsync();
        }
    }
}