using System;
using System.Collections.Generic;
using System.Text;
using Demo4NER.Models;
using Xamarin.Forms;

namespace Demo4NER
{
    public class BusinessTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BusinessDataTemplateWithDistance{ get; set; }
        public DataTemplate BusinessDataTemplateWithoutDistance { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((Business) item).Distance.Equals(0)
                ? BusinessDataTemplateWithoutDistance
                : BusinessDataTemplateWithDistance;
        }
    }
}
