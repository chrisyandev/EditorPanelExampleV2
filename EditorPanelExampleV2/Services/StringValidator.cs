using Avalonia.Controls;
using System.Text.RegularExpressions;

namespace EditorPanelExampleV2.Services
{
    public static class StringValidator
    {
        /// <summary>
        /// Returns true if text is formatted like a floating point number
        /// </summary>
        public static bool IsFloat(string text)
        {
            Regex regex = new(@"^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$");
            return regex.IsMatch(text);
        }

        /// <summary>
        /// Returns true if string matches one of the strings in the array
        /// </summary>
        public static bool DoesStringMatchAnyStrings(string text, string[] strings)
        {
            foreach (string s in strings)
            {
                if (s == text)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// If input exceeds limits, input is set to previous valid input
        /// </summary>
        public static void PostInputCheckIfExceedsLimits(TextBox textBox,
            float previousValue, float min = float.NaN, float max = float.NaN)
        {
            if (float.TryParse(textBox.Text, out float input))
            {
                // No limits provided
                if (float.IsNaN(min) && float.IsNaN(max))
                {
                    textBox.Text = previousValue.ToString();
                }
                // Both min and max provided
                else if (!float.IsNaN(min) && !float.IsNaN(max))
                {
                    if (input < min)
                    {
                        textBox.Text = min.ToString();
                    }
                    else if (input > max)
                    {
                        textBox.Text = max.ToString();
                    }
                    else
                    {
                        textBox.Text = previousValue.ToString();
                    }
                }
                // Only min provided
                else if (!float.IsNaN(min) && float.IsNaN(max))
                {
                    if (input < min)
                    {
                        textBox.Text = min.ToString();
                    }
                    else
                    {
                        textBox.Text = previousValue.ToString();
                    }
                }
                // Only max provided
                else if (float.IsNaN(min) && !float.IsNaN(max))
                {
                    if (input > max)
                    {
                        textBox.Text = max.ToString();
                    }
                    else
                    {
                        textBox.Text = previousValue.ToString();
                    }
                }
            }
            else
            {
                // Could not parse float
                textBox.Text = previousValue.ToString();
            }
        }
    }
}
