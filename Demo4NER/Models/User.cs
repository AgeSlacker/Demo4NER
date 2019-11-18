
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
        public string Name { get; set; }/*
        
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string Nacionality { get; set; }
        public string Contact { get; set; }
        [NotMapped]
        public List<Business> Businesses { get; set; }
        [NotMapped]
        public List<Service> Services{ get; set; }
        public DateTime LastLogin { get; set; }*/

    }
}