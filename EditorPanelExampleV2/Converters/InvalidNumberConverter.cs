using Avalonia.Data.Converters;
using Avalonia.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EditorPanelExampleV2.Converters
{
    public class InvalidNumberConverter : IValueConverter
    {
        // From the ViewModel value to the UI value (e.g., when loading data)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        // From the UI value back to the ViewModel value (e.g., when user inputs data)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;

            if (string.IsNullOrEmpty(strValue))
            {
                return BindingOperations.DoNothing; // Indicates that no updates should be made to the source property
            }

            if (!float.TryParse(strValue, out float fltValue))
            {
                return BindingOperations.DoNothing;
            }

            return fltValue; // Passed validation
        }
    }
}
