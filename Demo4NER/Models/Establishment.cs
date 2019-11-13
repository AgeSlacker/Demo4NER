using System;
using System.Collections.Generic;
using System.Text;

namespace Demo4NER.Models
{
    public class Establishment : Bussiness
    {
        public CategoryType Category { get; set; }

        public enum CategoryType
        {
            BAR, CAFE // TODO: completar
        }
    }
}
