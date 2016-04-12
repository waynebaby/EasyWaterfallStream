using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace EasyWaterfallStream
{
    public class OrientationFlipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Orientation && targetType == typeof(Orientation))
            {
                var ov = (Orientation)value;
                switch (ov)
                {
                    case Orientation.Vertical:
                        return Orientation.Horizontal;

                    case Orientation.Horizontal:
                        return Orientation.Vertical;
                    default:
                        break;
                }
            }
            throw new InvalidOperationException("this converter only support Orientation to Orientation convert");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return Convert(value, targetType, parameter, language);
        }
    }
}
