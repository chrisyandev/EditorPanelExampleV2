using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;
using EditorPanelExampleV2.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;
using Avalonia.Interactivity;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Avalonia;

namespace EditorPanelExampleV2.Views
{
    public partial class NewMaterialWindow : ReactiveWindow<NewMaterialViewModel>
    {
        private TextBox _materialInputTextBox;

        public NewMaterialWindow()
        {
            InitializeComponent();

            _materialInputTextBox = this.FindControl<TextBox>("materialInputTextBox");
            _materialInputTextBox.AttachedToVisualTree += (s, e) => _materialInputTextBox.Focus();

            this.WhenActivated(d => d(ViewModel!.AddCommand.Subscribe(CloseIfInputValid)));
        }

        private void CloseIfInputValid(object dialogResult)
        {
            if (dialogResult is string input && input.Trim() != string.Empty)
            {
                Close(dialogResult);
            }
            else
            {
                ViewModel!.InvalidInputMessage = new List<string> { "Invalid input" };
            }
        }

        public void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
