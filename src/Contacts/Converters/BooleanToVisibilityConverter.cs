﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Contacts.Converters
{
    // Converts bool to Visibility (Visible/Collapsed) and back
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility visibility && visibility == Visibility.Visible;
        }
    }
}