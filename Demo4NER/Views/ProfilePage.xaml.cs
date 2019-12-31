/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo4NER.ViewModels;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfileViewModel viewModel;
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ProfileViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadUserCommand.Execute(null);
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditProfilePage());
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NewBusinessPage());
        }
    }
}*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo4NER.ViewModels;
using Demo4NER.Models;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfileViewModel viewModel;
        public User NewUser { get; set; } = new User();
        public List<Review> Reviews { get; set; } = new List<Review>();
        public Review RevA { get; set; } = new Review();
        public Review RevB { get; set; } = new Review();
        public Business BusinessA { get; set; } = new Business();
        public Business BusinessB { get; set; } = new Business();

        public ProfilePage()
        {
            InitializeComponent();

            //DUMMY USER WITH DUMMY REVIEWS LIST
            NewUser.Name = "Fernandinho Silveira";
            NewUser.Nationality = "Brasileira";
            NewUser.Email = "fernandinho.silveirinho@gmail.com";
            NewUser.Contact = "912313452";
            RevA.Comment = "Este sitio é brutal.";
            RevA.Rating = 5;
            RevA.User = NewUser;
            RevA.Id = 1;
            BusinessA.Name = "FixePizza";
            RevA.Business = BusinessA;
            RevA.BusinessName = BusinessA.Name;
            RevB.Comment = "Nada de especial.";
            RevB.Rating = 3;
            RevB.User = NewUser;
            RevB.Id = 2;
            BusinessB.Name = "SomeOtherStore";
            RevB.Business = BusinessB;
            RevB.BusinessName = BusinessB.Name;
            Reviews.Add(RevA);
            Reviews.Add(RevB);
            Reviews.Add(RevA);
            Reviews.Add(RevB);
            Reviews.Add(RevA);
            Reviews.Add(RevB);
            Reviews.Add(RevA);
            Reviews.Add(RevB);
            Reviews.Add(RevA);
            Reviews.Add(RevB);
            Reviews.Add(RevA);
            Reviews.Add(RevB);
            Reviews.Add(RevA);
            Reviews.Add(RevB);
            Reviews.Add(RevA);
            Reviews.Add(RevB);
            NewUser.Reviews = Reviews;

            //DAR USER COMO ARGUMENTO
            BindingContext = viewModel = new ProfileViewModel(NewUser);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //viewModel.LoadUserCommand.Execute(null);
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditProfilePage());
        }
    }
}