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
        private Image _image = null;
        [NotMapped]
        public Image Image
        {
            get
            {
                if (_image == null && BusinessImage != null)
                {
                    _image = new Image()
                    {
                        Source = ImageSource.FromStream(() => new MemoryStream(BusinessImage))
                    };
                }
                return _image;

            }
            set => _image = value;
        }
        public string Description { get; set; }
        public string Contact { get; set; }
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