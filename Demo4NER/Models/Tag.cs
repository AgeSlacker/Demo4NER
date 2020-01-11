using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo4NER.Models
{
    public class Tag
    {
        [Key] 
        public int TagId { get; set; }
        public string Value { get; set; }
        public ICollection<BusinessTag> BusinessTags { get; set; } // uma tag pode ter muitas services
    }
}