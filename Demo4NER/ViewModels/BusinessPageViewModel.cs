using Demo4NER.Models;
using Demo4NER.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class BusinessPageViewModel : BaseViewModel
    {
        private Business _business;
        public String ReviewComment { get; set; }
        public String ReviewRating { get; set; }
        public User LoggedUser;
        public Boolean IsLogged;

        public Business Business
        {
            get => _business;
            set => SetProperty(ref _business, value);
        }
        public Command LoadBusinessCommand { get; set; }
        public Command NavigateToMapViewCommand { get; set; }

        public BusinessPageViewModel(Business selectedBusiness)
        {
            Business = selectedBusiness;
            ReviewComment = "";
            ReviewRating = "";

            LoggedUser = (Application.Current as App).GetUserFromProperties();
            if(LoggedUser != null)
            {
                IsLogged = true;
            }
            Business.Links = new List<Link>();
            Link link1 = new Link() { Name = "Site", URL = "4nerapp.com" };
            Link link2 = new Link() { Name = "Facebook", URL = "facebook.com/4nerapp" };

            Business.Schedule = "Dias de semana: 12h-15h 19h-00h\nFim de semana: 19h - 00h";
            //double rating1 = double.Parse("4,9");
            //double rating2 = double.Parse("6");
/*            Review review1 = new Review()
            {
                Id = 0,
                User = null,
                Business = null,
                BusinessName = null,
                Rating = double.Parse("4,9"),
                Comment = "Pretty Cool."
            };
            Review review2 = new Review()
            {
                Id = 1,
                User = null,
                Business = null,
                BusinessName = null,
                Rating = double.Parse("6"),
                Comment = "Dope."
            };
            Business.Reviews = new ObservableCollection<Review>() { review1, review2 };*/
            Business.Links.Add(link1); 
            Business.Links.Add(link2);
            Business.Schedule = "Dias de semana: 12h-15h 19h-00h\nFim de semana: 19h - 00h";

            NavigateToMapViewCommand = new Command(() =>
            {
                MessagingCenter.Send<BaseViewModel,Business>(this,"navigate",Business);
            });
        }
    }
}
