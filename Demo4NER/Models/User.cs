
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Xamarin.Forms;
using System.IO;
using Newtonsoft.Json;

namespace Demo4NER.Models
{
    public class User
    {

        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        
        public string Email { get; set; }

        public string Password { get; set; }
        
        public string Nationality { get; set; }
        public string Contact { get; set; }
        public byte[] UserImage { get; set; }

        private ImageSource _imageSource = null;
        [NotMapped]
        [JsonIgnore]
        public ImageSource ImageSource
        {
            get
            {
                if (_imageSource == null && UserImage != null)
                {
                    _imageSource = ImageSource.FromStream(() => new MemoryStream(UserImage));
                }
                return _imageSource;
            }
            set => _imageSource = value;
        }
        public List<Business> Businesses { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Service> Services{ get; set; }
        DateTime? lastLogin;
        public DateTime? LastLogin
        {
            get => lastLogin.HasValue ? lastLogin : DateTime.MinValue;
            set => lastLogin = DateTime.Now;
        }

    }
}