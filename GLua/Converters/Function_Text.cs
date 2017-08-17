using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using GLua.Models;

namespace GLua.Converters
{
    class Function_Text : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string target = parameter as string;
            if (value is Function)
            {
                Function s = value as Function;
                switch (target)
                {
                    case "Name":
                        return s.Name + "(";
                    case "Args":
                        if (s.Args == null)
                        {
                            return ")";
                        }
                        StringBuilder sb = new StringBuilder();
                        foreach (string[] arg in s.Args)
                        {
                            foreach (string st in arg)
                            {
                                sb.Append(st + " ");
                            }
                        }
                        return sb.ToString()+")";
                }               
            }
            return "";
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
