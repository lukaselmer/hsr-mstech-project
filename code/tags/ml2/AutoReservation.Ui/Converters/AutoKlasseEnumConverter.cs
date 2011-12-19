#region

using System;
using System.Globalization;
using System.Windows.Data;
using AutoReservation.Common.DataTransferObjects;

#endregion

namespace AutoReservation.Ui.Converters
{
    public class AutoKlasseEnumConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (AutoKlasse) value;
        }

        #endregion
    }
}