using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessPage : ContentPage
    {
        public BusinessPage()
        {
            InitializeComponent();
            BindingContext = this;

            Models.Establishment fixePizza = new Models.Establishment
            {
                Name = "Fixeizza",
                Category = Models.Establishment.CategoryType.BAR,
                Description = "Lorem Ipsum",
                Contact = "934 957 565",
                Email = "fixepizza@gmail.com",
                Rating = 4.9F
            };

            businessName.Text = fixePizza.Name;
            businessCategory.Text = fixePizza.Category.ToString();
            businessDescription.Text = fixePizza.Description;
            businessContact.Text = fixePizza.Contact;
            businessEmail.Text = fixePizza.Email;
            businessRating.Text = fixePizza.Rating.ToString();

        }
    }
}