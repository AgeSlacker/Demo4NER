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
            Nats.Add(new Nationality
            {
                Name = "Brasileira",
                Country = "Brasil",
                ImageSource = "br.jpg"
            });
            Nats.Add(new Nationality
            {
                Name = "Portuguesa",
                Country = "Portugal",
                ImageSource = "pt.jpg"
            });
            Nats.Add(new Nationality
            {
                Name = "Ucraniana",
                Country = "Ucrania",
                ImageSource = "ua.jpg"
            });
        }
    }
}
