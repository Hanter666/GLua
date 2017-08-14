using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace GLua.Converters
{
    class Text_Color : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value as string;
            Color c = Color.FromRgb(255,255,255);
            switch (s)
            {
                case "client":
                    c = Color.FromRgb(255, 136, 0);
                    break;
                case "server":
                    c = Color.FromRgb(0, 136, 255);
                    break;
                case "menu":
                    c = Color.FromRgb(0, 170, 0);
                    break;
                case "shared":
                    c = Color.FromRgb(255, 136, 255);
                    break;
                case "client_m":
                    c = Color.FromRgb(170, 170, 0);
                    break;
                case "shared_m":
                    c = Color.FromRgb(170, 170, 100);
                    break;
            }
            SolidColorBrush color = new SolidColorBrush(c);
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
