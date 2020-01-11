using System;
using System.Collections.Generic;

namespace Demo4NER.Models
{
    public class Service : Business
    {
        public List<Tag> Tags { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}