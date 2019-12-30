using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo4NER.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void ProfileRequest()
        {
         //   if(user is logged)
         //   {
         //     Navigation.PushModalAsync(new ProfilePage());
         //   } else
         //   {
                Navigation.PushModalAsync(new LoginPage());
         //   }
        }
    }
}