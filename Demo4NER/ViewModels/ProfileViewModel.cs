/*using Demo4NER.Models;
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
        public User User { get; set; } = new User();
        //public ObservableCollection<Review> Reviews { get; set; } = new ObservableCollection<Review>();
        public Command LoadUserCommand { get; set; }
        //public Command LoadReviewsCommand { get; set; }
        public ProfileViewModel() 
        {
            LoadUserCommand = new Command(async () => await LoadUserCommandExecute());
            //LoadReviewsCommand = new Command(async () => await LoadReviewsCommandExecute());
        }
        private async Task LoadReviewsCommandExecute()
        {
            using (var db = new criar context para reviews)
            {
                var _reviews = await db.Reviews.ToListAsync();
                Reviews.Clear();
                foreach (Review review in _reviews)
                    Reviews.Add(review);
            }
        }
        private async Task LoadUserCommandExecute()
        {
            using (var db = new NerContext()) {
                User = await db.Users.Where(s => s.Name.Equals("Ricardo")).FirstOrDefaultAsync<User>();
            }
        }
    }
}*/
using Demo4NER.Models;
using Demo4NER.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content.Res;
using Xamarin.Forms;
using System.Diagnostics;

namespace Demo4NER.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private User _user;

        public User User
        {
            get => _user;
            set
            {
                // Corre sempre que a página atualiza
                if (value != null)
                {
                    //tamanho 30/20
                    value.Contact = "912313452";
                    Business BusinessA = new Business { Name = "FixePizza" };
                    Review RevA = new Review
                    {
                        Comment = "Este sitio é brutal.",
                        Rating = 5,
                        User = value,
                        Id = 1,
                        Business = BusinessA,
                        BusinessName = BusinessA.Name
                    };
                    Business BusinessB = new Business { Name = "SomeOtherStore" };
                    Review RevB = new Review
                    {
                        Comment = "Nada de especial.",
                        Rating = 3,
                        User = value,
                        Id = 2,
                        Business = BusinessB,
                        BusinessName = BusinessB.Name
                    };
                    value.Reviews = new List<Review>
                    {
                        RevA,
                        RevB,
                        RevA,
                        RevB,
                        RevA,
                        RevB,
                        RevA,
                        RevB,
                        RevA,
                        RevB,
                        RevA,
                        RevB,
                        RevA,
                        RevB,
                        RevA,
                        RevB
                    };
                    string temp;
                    if (!Nationality.TryGetValue(value.Nationality, out temp))
                    {
                        ImageSource = "earth.png";
                    }
                    else
                    {
                        ImageSource = temp;
                        Debug.WriteLine(ImageSource);
                    }
                    Debug.WriteLine(value.Nationality);
                    SetProperty(ref _user, value);
                }
            }
        }

        private string imageSource;
        public string ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged();
            }
        }

        Dictionary<string, string> Nationality = new Dictionary<string, string>();

        //public ObservableCollection<Review> Reviews { get; set; } = new ObservableCollection<Review>();

        public Command LoadUserCommand { get; set; }

        //public Command LoadReviewsCommand { get; set; }

        public ProfileViewModel(User user)
        {
            User = user;
            Nationality.Add("Brasileira", "br.png");
            Nationality.Add("Portuguesa", "pt.png");
            Nationality.Add("Ucraniana", "ua.png");
        }

        /*private async Task LoadReviewsCommandExecute()
        {
            using (var db = new criar context para reviews
            {
                var _reviews = await db.Reviews.ToListAsync();
                Reviews.Clear();
                foreach (Review review in _reviews)
                    Reviews.Add(review);
            }
        }*/

        private async Task LoadUserCommandExecute()
        {
            using (var db = new NerContext())
            {
                User = await db.Users.Where(s => s.Name.Equals("test")).FirstOrDefaultAsync<User>();
            }
        }

    }
}