using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Demo4NER.Models
{
    public class Business
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public byte[] BusinessImage { get; set; }
        private ImageSource _imageSource = null;
        [NotMapped]
        public ImageSource ImageSource
        {
            get
            {
                if (_imageSource == null && BusinessImage != null)
                {
                    _imageSource = ImageSource.FromStream(() => new MemoryStream(BusinessImage));
                }
                return _imageSource;

            }
            set => _imageSource = value;
        }
        public string Description { get; set; }
        public string Contact { get; set; }
        public Category Category { get; set; }

        private Location _location = new Location();
        [NotMapped]
        public Location Location
        {
            get => _location;
            set
            {
                Longitude = value.Longitude;
                Latitude = value.Latitude;
                _location = value;
            }
        }
        public double Latitude
        {
            get => _location.Latitude;
            set => _location.Latitude = value;
        }
        public double Longitude
        {
            get => _location.Longitude;
            set => _location.Longitude = value;
        }
        [NotMapped]
        public double Distance { get; set; }
        public string WrittenAddress { get; set; }
        public string Email { get; set; }
        public ICollection<Feature> Features { get; set; }
        public bool IsFeatured { get; set; }
        public bool HasDiscounts { get; set; }
        public DateTime FeaturedEndDate { get; set; }
        public ICollection<Promotion> Promotions { get; set; }
        public ICollection<Discount> Discounts { get; set; }
        public ICollection<Click> Clicks { get; set; }
        public float Rating { get; set; }
        public ICollection<Link> Links { get; set; }
        public String Schedule { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public String Nationality { get; set; }
        public ICollection<BusinessTag> BusinessTags { get; set; }
    }
}