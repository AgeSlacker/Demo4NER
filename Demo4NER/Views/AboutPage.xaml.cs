using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Demo4NER.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void FacebookHandler(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.facebook.com/4NERapp/"));
        }

        private void InstagramHandler(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.instagram.com/4nerapp/"));
        }

        private void WebsiteHandler(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.4nerapp.com/"));
        }
    }
}