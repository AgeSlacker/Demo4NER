using System;
using System.Collections.Generic;
using System.Text;

namespace Demo4NER.Models
{
    public class Nationality
    {
        public string Name { get; set; }
        public Nationality(string v)
        {
            this.Name = v;
        }
    }
}
