using System;
using System.Globalization;
using System.Windows.Data;

namespace View.ViewModel
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
        /// <param name="values">The array of values to convert</param>
        /// <param name="targetType">The type of the binding target property</param>
        /// <param name="parameter">The converter parameter to use</param>
        /// <param name="culture">The culture to use in the converter</param>
        /// <returns>
        /// false if any input value is true, 
        /// true if all values are false or empty collection
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when any value is not a boolean type
        /// </exception>
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