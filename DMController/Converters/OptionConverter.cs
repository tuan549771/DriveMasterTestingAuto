using DMController.Views;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DMController.Converters 
{
    public class OptionConverter : IValueConverter
    {
        #region IValueConverter Members


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "TypeTime":
                    {
                        TimeCaptionControl captionTime = new TimeCaptionControl();
                        return captionTime;
                    }
                default:
                    {
                        CaptionControl caption = new CaptionControl();
                        return caption;
                    }
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}
