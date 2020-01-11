using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo4NER.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchControlPage : ContentPage
    {
        private BaseBusinessListViewModel viewModel;
        public SearchControlPage(BaseBusinessListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            BaseBusinessListViewModel.InitializeTagsCommand.Execute(null);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            viewModel.SelectedFilterIndexes = TagsComboBox.SelectedIndices as List<int>;
            Navigation.PopModalAsync();
        }

        private void SfComboBox_OnDropDownOpen(object sender, EventArgs e)
        {
            Debug.WriteLine("test");
        }
    }
}