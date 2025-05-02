using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ProgressBarDemo.Helpers
{
    public class BooleanToOppositeBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return false;
        }
    }

    public class IndeterminateButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isIndeterminate)
            {
                return isIndeterminate ? "切换到确定模式" : "切换到不确定模式";
            }
            return "切换模式";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IndeterminateModeTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isIndeterminate)
            {
                return isIndeterminate ? "不确定模式（IsIndeterminate = True）" : "确定模式（IsIndeterminate = False）";
            }
            return "未知模式";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StyleSelectorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string selectedStyle && parameter is string styleName)
            {
                return selectedStyle == styleName ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long bytes)
            {
                return FormatBytes(bytes);
            }
            return "未知大小";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string FormatBytes(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int counter = 0;
            double number = bytes;
            
            while (number >= 1024 && counter < suffixes.Length - 1)
            {
                number /= 1024;
                counter++;
            }
            
            return $"{number:0.0} {suffixes[counter]}";
        }
    }
} 