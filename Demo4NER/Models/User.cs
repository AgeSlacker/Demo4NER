
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace Demo4NER.Models
{
    public class User
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Nacionality { get; set; }
        public string Contact { get; set; }
        public List<Bussiness> Bussinesses { get; set; }
        public List<Service> Services{ get; set; }
        public DateTime LastLogin { get; set; }

    }
}