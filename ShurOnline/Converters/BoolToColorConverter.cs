﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ShurOnline.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (int) value;
            var convertFromString = ColorConverter.ConvertFromString(ColorsProvider.Colors[boolValue]);
            if (convertFromString != null)
                return new SolidColorBrush((System.Windows.Media.Color)convertFromString);
            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}