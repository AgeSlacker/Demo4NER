using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo4NER.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public String Value { get; set; }
        public ICollection<Business> Businesses { get; set; }
    }
}
