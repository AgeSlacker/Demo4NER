using Demo4NER.Models;
using Demo4NER.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class BusinessPageViewModel : BaseViewModel
    {
        private Business _business;
        public String ReviewComment
        {
            get => _reviewComment;
            set => SetProperty(ref _reviewComment,value);
        }
        public float ReviewRating
        {
            get => _reviewRating;
            set => SetProperty(ref _reviewRating,value);
        }

        public bool UserAlreadyCommented { get; set; } // TODO restrict one comment per user

        public User LoggedUser;
        public event EventHandler ReviewsUpdated;
        public event EventHandler UserAlreadyReviewed;
        public bool IsLogged
        {
            get => _isLogged;
            set => SetProperty(ref _isLogged, value);
        } 

        public Review NewReview;
        private bool _isLogged = false;
        private string _reviewComment;
        private float _reviewRating;

        public ObservableCollection<Review> Reviews { get; set; } = new ObservableCollection<Review>();

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

            foreach (Review businessReview in Business.Reviews)
            {
                Reviews.Add(businessReview);
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
            if (LoggedUser != null) IsLogged = true;
            PostCommentCommand = new Command(async() => await PostCommentAsync());
        }
        
        public async Task PostCommentAsync()
        {
            if (Business.Reviews.Any(r=>r.User.UserId == LoggedUser.UserId))
            {
                OnUserAlreadyReviewed();
                return; // TODO change review
            }
            NewReview = new Review()
            {
                //Id = 10,
                User = LoggedUser,
                Business = Business,
                Rating = ReviewRating,
                Comment = ReviewComment
            };
            Device.BeginInvokeOnMainThread(() =>
            {
                Business.Reviews.Add(NewReview);
                Reviews.Add(NewReview); // TODO why this works
                OnReviwsUpdated();
            });
            ReviewComment = "";
            ReviewRating = 0;

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

        protected virtual void OnReviwsUpdated()
        {
            ReviewsUpdated?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnUserAlreadyReviewed()
        {
            UserAlreadyReviewed?.Invoke(this, EventArgs.Empty);
        }
    }
}
