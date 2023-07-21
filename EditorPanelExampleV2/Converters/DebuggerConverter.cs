using Avalonia.Data.Converters;
using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;

namespace EditorPanelExampleV2.Converters
{
    public class DebuggerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Set breakpoint here
            if (value is IEnumerable && value is not string)
            {
                foreach (var item in value as IEnumerable)
                {
                    Debug.WriteLine(item);
                }
            }
            else
            {
                Debug.WriteLine(value);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Set breakpoint here
            if (value is IEnumerable && value is not string)
            {
                foreach (var item in value as IEnumerable)
                {
                    Debug.WriteLine(item);
                }
            }
            else
            {
                Debug.WriteLine(value);
            }

            return value;
        }
    }
}
