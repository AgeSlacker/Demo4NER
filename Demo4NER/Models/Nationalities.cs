using System;
using System.Collections.Generic;
using System.Text;

namespace Demo4NER.Models
{
    public class Nationalities
    {
        public static List<Nationality> Nats { get; private set; }

        static Nationalities()
        {
            Nats = new List<Nationality>();
        }
    }
}
