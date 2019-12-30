using Demo4NER.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo4NER.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
    {
        public IList<Nationality> Nationals { get { return Nationalities.Nats; } }
        public Nationality selectedNat
        {   get { return selectedNat; }
            set {
                if (selectedNat != value) {
                    selectedNat = value;
                    imageSource = selectedNat.ImageSource;
                    OnPropertyChanged();
                }
            }
        }
        public string imageSource { get; set;}

    }
}
