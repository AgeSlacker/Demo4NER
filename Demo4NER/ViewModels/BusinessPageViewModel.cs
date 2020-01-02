using Demo4NER.Models;
using Demo4NER.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class BusinessPageViewModel : BaseViewModel
    {
        private Business _business;

        public Business Business
        {
            get => _business;
            set => SetProperty(ref _business, value);
        }
        public Command LoadBusinessCommand { get; set; }

        public BusinessPageViewModel(Business selectedBusiness)
        {
            Business = selectedBusiness;
            Business.Links = new List<Link>();
            Link link1 = new Link() { Name = "Site", URL = "4nerapp.com" };
            Link link2 = new Link() { Name = "Facebook", URL = "facebook.com/4nerapp" };
            Business.Links.Add(link1); Business.Links.Add(link2);
            Business.Schedule = "Dias de semana: 12h-15h 19h-00h\nFim de semana: 19h - 00h";         
            float rating1 = (float)Convert.ToDouble("4,900");
            float rating2 = (float)Convert.ToDouble("6");
            Business.Reviews = new List<Review>();
            Review review1 = new Review() { Id = 1,
                                            User = null,
                                            Rating = rating1,
                                            Comment = "Pretty good."
            };
            Review review2 = new Review() { Id = 2,
                                            User = null,
                                            Rating = rating2,
                                            Comment = "Dope." };
            Business.Reviews.Add(review1);
            Business.Reviews.Add(review2);
            //business.Name = "Fixepizza";
            //business.Description = "Pizzaria";
            //business.Contact = "961234567";
            //business.Email = "fixepizza@gmail.com";
        }
    }
}
