﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;
using System.Windows;

namespace CommonTools.Converter
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanVisibilityValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (((bool)value) == true)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            throw new Exception("The method or operation is not implemented.");

        }


    }
}
