using System;
using System.Collections.Generic;
using System.Text;

namespace Demo4NER.Models
{
    public class Nationalities
    {
        public static IList<Nationality> Nats { get; private set; }

        static Nationalities() {
            Nats = new List<Nationality>();
        }
    }
}
