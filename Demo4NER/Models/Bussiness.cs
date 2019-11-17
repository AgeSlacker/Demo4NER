using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xamarin.Essentials;

namespace Demo4NER.Models
{
    public class Bussiness
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Contact { get; set; }
        public Location Location { get; set; }
        public string Email { get; set; }
        public List<Feature> Features { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime FeaturedEndDate{ get; set; }
        public List<Promotion> Promotions { get; set; }
        public List<Discount> Discounts { get; set; }
        public List<Click> Clicks { get; set; }
        public float Rating { get; set; }
        public List<Link> Links { get; set; }

    }
}