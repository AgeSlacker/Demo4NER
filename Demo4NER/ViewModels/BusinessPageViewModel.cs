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
            NavigateToMapViewCommand = new Command(() =>
            {
                MessagingCenter.Send<BaseViewModel,Business>(this,"navigate",Business);
            });

            Business = selectedBusiness;
            LoggedUser = (Application.Current as App).GetUserFromProperties();
            if(LoggedUser != null)
            {
                IsLogged = true;
            }
            PostCommentCommand = new Command(async() => await PostCommentAsync());
        }
        
        public async Task PostCommentAsync()
        {
            if (Business.Reviews == null)
            {
                Business.Reviews = new ObservableCollection<Review>();
            }

            NewReview = new Review()
            {
                //Id = 10,
                User = LoggedUser,
                Business = Business,
                Rating = double.Parse(ReviewRating),
                Comment = ReviewComment
            };

            Business.Reviews.Add(NewReview);
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
                    db.Reviews.Update(NewReview);
                    db.Businesses.Update(Business);
                    db.SaveChanges();
                    OnPropertyChanged();
                }
            });
        }

        //public async Task LoadReviewsAsync()
        //{
        //    await Task.Run(async () =>
        //    {
        //        using (var db = new NerContext())
        //        {
        //            //var reviews = db.Reviews.SqlQuery("SELECT * FROM reviews WHERE BusinessId = " + Business.Id);

        //            Debug.WriteLine("0");
        //            if(Business.Reviews != null)
        //            {
        //                Business.Reviews.Clear();
        //            }
        //            else
        //            {
        //                Business.Reviews = new ObservableCollection<Review>();
        //            }
        //            Debug.WriteLine("1");
        //            var reviews = await db.Reviews.ToListAsync();
        //            Debug.WriteLine("2");
        //            foreach (Review review in reviews)
        //            {
        //                Debug.WriteLine(review.Business.ToString());
        //                if(review.Business.Id == Business.Id)
        //                {
        //                    Business.Reviews.Add(review);
        //                }
        //            }
        //            Debug.WriteLine("Num= " + Business.Reviews.Count);
        //            OnPropertyChanged();
        //        }
        //    });
        //}
    }
}
