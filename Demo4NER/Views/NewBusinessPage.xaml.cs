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
    public partial class NewBusinessPage : ContentPage
    {
        NewBusinessViewModel viewModel;
        public NewBusinessPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new NewBusinessViewModel();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            viewModel.CreateNewBusinessCommand.Execute(null);
            Navigation.PopModalAsync();
        }
    }

}
