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
    public partial class BusinessPage : ContentPage { 
        public BusinessPage()
        {
            InitializeComponent();
            //BindingContext = this;

            Models.Business business = new Models.Business
            {
                Name = "FixePizza",
                Description = "Lorem Ipsum",
                Contact = "934 957 565",
                Location = null,
                Email = "fixepizza@gmail.com",
                Features = null,
                IsFeatured = false,
                Promotions = null,
                Discounts = null,
                Clicks = null,
                Rating = 4.9F,
                Links = null
            };

        Label businessName = new Label {Text = business.Name};


    }
    }
}