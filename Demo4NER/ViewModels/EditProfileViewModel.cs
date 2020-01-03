using Demo4NER.Models;
using Demo4NER.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
    {
        public IList<Nationality> Nationals { get; set; } = new List<Nationality>();

        private Nationality SelectedNat;
        public Nationality selectedNat
        {
            get { return SelectedNat; }
            set
            {
                if (SelectedNat != value)
                {
                    SelectedNat = value;
                    string temp;
                    if (!Nationality.TryGetValue(SelectedNat.Name, out temp))
                    {
                        ImageSource = "earth.png";
                    }
                    else
                    {
                        ImageSource = temp;
                    }
                    Index = Nationals.IndexOf(SelectedNat);
                    OnPropertyChanged();
                }
            }
        }

        public User NewUser { get; set; } = new User();

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

        private int index;
        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                OnPropertyChanged();
            }
        }

        Dictionary<string, string> Nationality = new Dictionary<string, string>();

        public Command EditProfileCommand { get; set; }

        public EditProfileViewModel(User MyUser)
        {

            NewUser = MyUser;
            SelectedNat = new Nationality(NewUser.Nationality);
            Nationals.Add(new Nationality("Brasileira"));
            Nationals.Add(new Nationality("Portuguesa"));
            Nationals.Add(new Nationality("Ucraniana"));
            Nationality.Add("Brasileira", "br.png");
            Nationality.Add("Portuguesa", "pt.png");
            Nationality.Add("Ucraniana", "ua.png");
            string temp;
            if (!Nationality.TryGetValue(NewUser.Nationality, out temp))
            {
                ImageSource = "earth.png";
            }
            else
            {
                ImageSource = temp;
            }
            Index = Nationals.IndexOf(SelectedNat);
            EditProfileCommand = new Command(async () => await EditProfileExecute());
        }
        private async Task EditProfileExecute()
        {
            NewUser.Nationality = selectedNat.Name;
            await Task.Run(() =>
            {
                using (var db = new NerContext())
                {
                    //db.Users.Add(NewUser); nao é add, devo substituir, not sure how to do it.
                    db.SaveChanges();
                }
            });

            (Application.Current as App).SaveUserInProperties(NewUser);
        }
    }
}
