using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace View.Services
{
    /// <summary>
    /// Provides methods for converting boolean array values.
    /// </summary>
    public class MultiValidationConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts an array of values to a boolean result.
        /// Returns true only if all input values are false.
        /// </summary>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 0)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] is bool value)
                    {
                        if (value)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Value is not a boolean type");
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Not implemented - returns null.
        /// </summary>
        /// <remarks>
        /// This is a one-way converter and doesn't support convert back functionality.
        /// </remarks>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
