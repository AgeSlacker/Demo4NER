﻿using System;
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
        public FakeAssProgressBarPage(uint timeMillis = 2000)
        {
            InitializeComponent();
            this.timeMillis = timeMillis;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ProgressBar.ProgressTo(1, timeMillis, Easing.CubicIn);
            //await Task.Delay(3000);
            await Navigation.PopAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return false;
        }
    }
}