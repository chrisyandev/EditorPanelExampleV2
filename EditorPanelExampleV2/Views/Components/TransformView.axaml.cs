using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using EditorPanelExampleV2.Services;
using EditorPanelExampleV2.ViewModels;
using System.Collections.Generic;

namespace EditorPanelExampleV2.Views
{
    public partial class TransformView : UserControl
    {
        private TextBox _previousSenderTextBox;

        public TransformView()
        {
            InitializeComponent();

            List<TextBox> textBoxes = new()
            {
                textBox_X,
                textBox_Y,
                textBox_Z
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
            // Allow entering '-' if not exist, or allow replacing existing '-'
            if (e.Text == "-")
            {
                if (!senderTextBox.Text.Contains('-')
                    || senderTextBox.SelectedText.Contains('-'))
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

            // Allow pasting string with '.' and '-' if not exist, or allow replacing existing string with both '.' and '-'
            if (clipBoardText.Contains('.') && clipBoardText.Contains('-'))
            {
                if (!(senderTextBox.Text.Contains('.') && senderTextBox.Text.Contains('-'))
                    || (senderTextBox.SelectedText.Contains('.') && senderTextBox.SelectedText.Contains('-')))
                {
                    e.Handled = false;
                    return;
                }
                e.Handled = true;
                return;
            }
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
            // Allow pasting string with '-' if not exist, or allow replacing existing string with '-'
            if (clipBoardText.Contains('-'))
            {
                if (!senderTextBox.Text.Contains('-')
                    || senderTextBox.SelectedText.Contains('-'))
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

            if (senderTextBox == textBox_X)
            {
                StringValidator.PostInputCheckIfExceedsLimits(textBox_X,
                    (textBox_X.DataContext as TransformViewModel).X);
            }
            else if (senderTextBox == textBox_Y)
            {
                StringValidator.PostInputCheckIfExceedsLimits(textBox_Y,
                    (textBox_Y.DataContext as TransformViewModel).Y);
            }
            else if (senderTextBox == textBox_Z)
            {
                StringValidator.PostInputCheckIfExceedsLimits(textBox_Z,
                    (textBox_Z.DataContext as TransformViewModel).Z);
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
