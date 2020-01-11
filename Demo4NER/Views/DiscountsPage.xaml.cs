using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo4NER.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class DiscountsPage : ContentPage
    {
        private DiscountsViewModel viewModel;
        public DiscountsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new DiscountsViewModel();
        }
    }
}