using System;
using System.Globalization;
using System.Windows.Data;

namespace ProgressBarDemo.Converters
{
    public class ProgressBarWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 4 || 
                !(values[0] is double value) || 
                !(values[1] is double minimum) || 
                !(values[2] is double maximum) || 
                !(values[3] is double actualWidth))
            {
                return 0d;
            }

            // 如果值超出范围，则限制在范围内
            if (value < minimum) value = minimum;
            if (value > maximum) value = maximum;
            
            // 计算进度比例
            double range = maximum - minimum;
            double valuePercentage = range <= 0 ? 0 : (value - minimum) / range;
            
            // 计算宽度
            return actualWidth * valuePercentage;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
} 