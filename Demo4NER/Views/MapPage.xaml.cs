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
    public partial class MapPage : ContentPage
    {
        private MapViewModel viewModel;
        public MapPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MapViewModel();
        }
    }
}