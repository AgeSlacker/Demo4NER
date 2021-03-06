﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Demo4NER.Models;
using Demo4NER.ViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;
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
            MessagingCenter.Subscribe<BaseViewModel, Business>(this, "navigate", async (sender, bus) => 
            {
                foreach (Page page in Navigation.ModalStack)
                {
                    await Navigation.PopModalAsync();
                }
                CurrentPage = Children[2];
                SelectedItem = Children[2];
            });

            // check if logged or not
            if ((Application.Current as App).GetUserFromProperties() != null)
            {
                // logged
                var profileNavigationPage = new NavigationPage(new ProfilePage())
                {
                    IconImageSource = ImageSource.FromFile("avatar.png"), Title = "Perfil"
                };
                Children.Add(profileNavigationPage);
            }
            else
            {
                var aboutNavigationPage = new NavigationPage(new LoginPage())
                {
                    IconImageSource = ImageSource.FromFile("avatar.png"), Title = "Login"
                };
                Children.Add(aboutNavigationPage);
            }
        }

        protected override void OnAppearing()
        { 
            base.OnAppearing();
        }
        protected override async void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            // Prompt login

            if (((NavigationPage)CurrentPage).RootPage.GetType() == typeof(MapPage))
            {
                if (!(Application.Current as App).Properties.ContainsKey("mapTipShowed"))
                {
                    await((NavigationPage)CurrentPage).RootPage.DisplayAlert("Dica",
                        "Podes carregar nos pins vermelhos e no nome do ponto de intere"
                        + "sse para ver mais detalhes ou abrir a navegação para o mapa.",
                        "Ok");
                    (Application.Current as App).Properties.Add("mapTipShowed", true);
                }
            }
        }
    }
}