using Demo4NER.Models;
using Demo4NER.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public User NewUser { get; set; } = new User();

        //public ObservableCollection<Review> Reviews { get; set; } = new ObservableCollection<Review>();

        public Command LoadUserCommand { get; set; }

        //public Command LoadReviewsCommand { get; set; }

        public ProfileViewModel() 
        {
            LoadUserCommand = new Command(async () => await LoadUserCommandExecute());
            //LoadReviewsCommand = new Command(async () => await LoadReviewsCommandExecute());
        }

        /*private async Task LoadReviewsCommandExecute()
        {
            using (var db = new criar context para reviews)
            {
                var _reviews = await db.Reviews.ToListAsync();
                Reviews.Clear();
                foreach (Review review in _reviews)
                    Reviews.Add(review);
            }
        }*/

        private async Task LoadUserCommandExecute()
        {
            using (var db = new NerContext()) {
                NewUser = await db.Users.Where(s => s.Name.Equals("Ricardo")).FirstOrDefaultAsync<User>();
            }
        }
    }
}
