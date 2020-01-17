using Demo4NER.Models;
using Demo4NER.Services;
using Microsoft.EntityFrameworkCore;
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

        public bool IsLogged
        {
            get => _isLogged;
            set => SetProperty(ref _isLogged, value);
        } 

        public Review NewReview;
        private bool _isLogged = false;

        public Business Business
        {
            get => _business;
            set => SetProperty(ref _business, value);
        }
        public Command LoadBusinessCommand { get; set; }
        public Command LoadReviewsCommand { get; set; }
        public Command NavigateToMapViewCommand { get; set; }
        public Command PostCommentCommand { get; set; }

        public BusinessPageViewModel(Business selectedBusiness)
        {
            Business = selectedBusiness;
            if (Business.Reviews == null)
            {
                Business.Reviews = new ObservableCollection<Review>();
            }

            if (Business.Links == null)
            {
                Business.Links = new ObservableCollection<Link>(); // TODO move this to model maybe ?
            }

            NavigateToMapViewCommand = new Command(() =>
            {
                MessagingCenter.Send<BaseViewModel,Business>(this,"navigate",Business);
            });

            LoggedUser = (Application.Current as App).GetUserFromProperties();
            if(LoggedUser != null)
            {
                IsLogged = true;
            }
            PostCommentCommand = new Command(async() => await PostCommentAsync());
        }
        
        public async Task PostCommentAsync()
        {
            NewReview = new Review()
            {
                //Id = 10,
                User = LoggedUser,
                Business = Business,
                Rating = double.Parse(ReviewRating),
                Comment = ReviewComment
            };
            Device.BeginInvokeOnMainThread(() =>
            {
                Business.Reviews.Add(NewReview);
            });
            ReviewComment = "";
            ReviewRating = "";

            await PostCommentExecute();
        }

        private async Task PostCommentExecute()
        {
            await Task.Run(() =>
            {
                using (var db = new NerContext())
                {
                    // TODO check if both needed
                    //db.Reviews.Update(NewReview);
                    db.Businesses.Update(Business);
                    db.SaveChanges();
                    //OnPropertyChanged(); // Acho que n faz nada
                }
            });
        }
    }
}
