using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo4NER.Models
{
    public class Click
    {
        
        [Key]
        public User User { get; set; }
        [Key]
        public DateTime Date { get; set; }
        
    }
}