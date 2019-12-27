using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xamarin.Essentials;

namespace Demo4NER.Models
{
    public class Business
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Contact { get; set; }
        Location location = new Location();
        [NotMapped]
        public Location Location
        {
            get => location;
            set
            {
                Longitude = value.Longitude;
                Latitude = value.Latitude;
                location = value;
            }
        }
        public double Latitude
        {
            get => location.Latitude;
            set => location.Latitude = value;
        }
        public double Longitude {
            get => location.Longitude;
            set => location.Longitude = value;
        }
        public  string WrittenAddress { get; set; }
        public string Email { get; set; }
        public List<Feature> Features { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime FeaturedEndDate { get; set; }
        public List<Promotion> Promotions { get; set; }
        public List<Discount> Discounts { get; set; }
        public List<Click> Clicks { get; set; }
        public float Rating { get; set; }
        public List<Link> Links { get; set; }
    }
}