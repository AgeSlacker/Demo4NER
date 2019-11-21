using System;
using System.Collections.Generic;
using System.Text;

namespace Demo4NER.Models
{
    public class Review
    {
        public int Id { get; set; }
        public User User { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
    }
}
