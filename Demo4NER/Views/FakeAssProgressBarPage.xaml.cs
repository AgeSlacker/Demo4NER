using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FakeAssProgressBarPage : ContentPage
    {
        private uint timeMillis;
        private bool rootSwaped;
        public FakeAssProgressBarPage(uint timeMillis = 2000, bool rootSwaped = false)
        {
            InitializeComponent();
            this.timeMillis = timeMillis;
            this.rootSwaped = rootSwaped;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ProgressBar.ProgressTo(1, timeMillis, Easing.CubicIn);
            //await Task.Delay(3000);
            if (rootSwaped) return;
            await Navigation.PopAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}