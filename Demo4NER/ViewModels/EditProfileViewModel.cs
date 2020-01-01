using Demo4NER.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Demo4NER.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
    {
        public IList<Nationality> Nationals { get; set; } = new List<Nationality>();

        private string SelectedNat;
        public string selectedNat
        {   get { return SelectedNat; }
            set {
                if (SelectedNat != value) {
                    SelectedNat = value;
                    string temp;
                    if (!Nationality.TryGetValue(SelectedNat, out temp))
                    {
                        ImageSource = "earth.png";
                    }
                    else
                    {
                        ImageSource = temp;
                    }
                    OnPropertyChanged();
                }
            }
        }

        public User NewUser { get; set; } = new User();

        public string ImageSource { get; set; }

        Dictionary<string, string> Nationality = new Dictionary<string, string>();

        public EditProfileViewModel(User MyUser)
        {
            NewUser = MyUser;
            //tamanho 30/20
            Nationals.Add(new Nationality("Brasileira"));
            Nationals.Add(new Nationality("Portuguesa"));
            Nationals.Add(new Nationality("Ucraniana"));
            Nationality.Add("Brasileira", "br.png");
            Nationality.Add("Portuguesa", "pt.png");
            Nationality.Add("Ucraniana", "ua.png");
            selectedNat = MyUser.Nationality;
            Debug.WriteLine(SelectedNat);
            string temp;
            if (!Nationality.TryGetValue(MyUser.Nationality, out temp))
            {
                ImageSource = "earth.png";
            }
            else {
                ImageSource = temp;
            }
        }
    }
}
