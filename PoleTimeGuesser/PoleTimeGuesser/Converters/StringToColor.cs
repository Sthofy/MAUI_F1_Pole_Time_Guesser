using Microsoft.Maui.Graphics.Converters;
using System.Globalization;

namespace PoleTimeGuesser.Converters
{
    internal class StringToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ColorTypeConverter converter = new ColorTypeConverter();

            Color color = (Color)(converter.ConvertFromInvariantString((string)value));

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string colorString = "Black";

            return colorString;
        }
    }
}
