using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using EditorPanelExampleV2.Models;
using EditorPanelExampleV2.Services;
using EditorPanelExampleV2.ViewModels;
using System.Collections.Generic;

namespace EditorPanelExampleV2.Views
{
    public partial class LightView : UserControl
    {
        private TextBox _previousSenderTextBox;

        public LightView()
        {
            InitializeComponent();

            List<TextBox> textBoxes = new()
            {
                textBox_Range,
                textBox_SpotAngle,
                textBox_Intensity,
                textBox_Strength,
                textBox_Bias
            };

            foreach (TextBox textBox in textBoxes)
            {
                textBox.AddHandler(TextInputEvent, TextBox_PreviewTextInput, RoutingStrategies.Tunnel);
                textBox.PastingFromClipboard += TextBox_PastingFromClipboard;
                textBox.LostFocus += TextBox_LostFocus;
                textBox.PointerReleased += TextBox_PointerReleased;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextInputEventArgs e)
        {
            TextBox senderTextBox = sender as TextBox;

            // Allow entering '.' if not exist, or allow replacing existing '.'
            if (e.Text == ".")
            {
                if (!senderTextBox.Text.Contains('.')
                    || senderTextBox.SelectedText.Contains('.'))
                {
                    e.Handled = false;
                    return;
                }
                e.Handled = true;
                return;
            }

            if (!StringValidator.IsFloat(senderTextBox.Text.Insert(senderTextBox.CaretIndex, e.Text)))
            {
                e.Handled = true;
                return;
            }

            e.Handled = false;
        }

        private async void TextBox_PastingFromClipboard(object sender, RoutedEventArgs e)
        {
            TextBox senderTextBox = sender as TextBox;
            string clipBoardText = await TopLevel.GetTopLevel(this)?.Clipboard.GetTextAsync();

            // Allow pasting string with '.' if not exist, or allow replacing existing string with '.'
            if (clipBoardText.Contains('.'))
            {
                if (!senderTextBox.Text.Contains('.')
                    || senderTextBox.SelectedText.Contains('.'))
                {
                    e.Handled = false;
                    return;
                }
                e.Handled = true;
                return;
            }

            if (!StringValidator.IsFloat(senderTextBox.Text.Insert(senderTextBox.CaretIndex, clipBoardText)))
            {
                e.Handled = true;
                return;
            }

            e.Handled = false;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            _previousSenderTextBox = null;

            TextBox senderTextBox = sender as TextBox;

            if (senderTextBox == textBox_Range)
            {
                StringValidator.PostInputCheckIfExceedsLimits(textBox_Range,
                    (textBox_Range.DataContext as LightViewModel).Range,
                    Light.MIN_RANGE,
                    float.NaN);
            }
            else if (senderTextBox == textBox_SpotAngle)
            {
                StringValidator.PostInputCheckIfExceedsLimits(textBox_SpotAngle,
                    (textBox_SpotAngle.DataContext as LightViewModel).SpotAngle,
                    Light.SPOTLIGHT_MIN_SPOT_ANGLE,
                    Light.SPOTLIGHT_MAX_SPOT_ANGLE);
            }
            else if (senderTextBox == textBox_Intensity)
            {
                StringValidator.PostInputCheckIfExceedsLimits(textBox_Intensity,
                    (textBox_Intensity.DataContext as LightViewModel).Intensity,
                    Light.MIN_INTENSITY,
                    float.NaN);
            }
            else if (senderTextBox == textBox_Strength)
            {
                StringValidator.PostInputCheckIfExceedsLimits(textBox_Strength,
                    (textBox_Strength.DataContext as LightViewModel).ShadowStrength,
                    Light.SHADOW_MIN_STRENGTH,
                    Light.SHADOW_MAX_STRENGTH);
            }
            else if (senderTextBox == textBox_Bias)
            {
                StringValidator.PostInputCheckIfExceedsLimits(textBox_Bias,
                    (textBox_Bias.DataContext as LightViewModel).ShadowBias,
                    Light.SHADOW_MIN_BIAS,
                    Light.SHADOW_MAX_BIAS);
            }
        }

        private void TextBox_PointerReleased(object sender, PointerReleasedEventArgs e)
        {
            if (e.InitialPressMouseButton == MouseButton.Left)
            {
                if (sender is TextBox senderTextBox && senderTextBox != _previousSenderTextBox)
                {
                    Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        senderTextBox.Focus();
                        senderTextBox.SelectAll();
                        _previousSenderTextBox = senderTextBox;
                    }, DispatcherPriority.Loaded);
                }
            }
        }
    }
}
