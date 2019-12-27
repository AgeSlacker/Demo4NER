
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo4NER.Models
{
    public class User
    {

        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string Nacionality { get; set; }
        public string Contact { get; set; }
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