using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Android.Locations;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Location = Xamarin.Essentials.Location;

namespace Demo4NER.Converters
{
    public class LocationToPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Xamarin.Essentials.Location location = (Location) value;
            return new Position(location.Latitude, location.Longitude);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
