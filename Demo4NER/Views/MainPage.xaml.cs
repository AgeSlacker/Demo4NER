using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Switch = Xamarin.Forms.Switch;

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

        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            // Prompt login
            if (((NavigationPage)CurrentPage).RootPage.GetType() == typeof(ProfilePage))
            {
                if ((Application.Current as App).GetUserFromProperties() == null)
                {
                    await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
                    CurrentPage = this.Children.First();
                    SelectedItem = this.Children.First();
                }
            }
        }
    }
}