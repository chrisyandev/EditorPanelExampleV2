using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;
using EditorPanelExampleV2.Controls;
using EditorPanelExampleV2.ViewModels;
using System;
using System.ComponentModel;

namespace EditorPanelExampleV2.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            mainScrollViewer.VerticalScrollBarExpanded += MainVerticalScrollBarExpanded;
            mainScrollViewer.VerticalScrollBarCollapsed += MainVerticalScrollBarCollapsed;
        }

        protected override void OnDataContextEndUpdate()
        {
            base.OnDataContextEndUpdate();

            (DataContext as MainWindowViewModel).PropertyChanged += MainWindow_PropertyChanged;
        }

        private void MainWindow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedComponentName")
            {
                // Assumes component was added causing a layout update
                // Otherwise event handler will remain subscribed until a random layout update
                mainScrollViewer.LayoutUpdated += ScrollViewerLayoutUpdated;
            }
        }

        private void MainVerticalScrollBarExpanded(object sender, MyScrollBarEventArgs e)
        {
            mainScrollViewer.FindDescendantOfType<StackPanel>().Margin = new Thickness(0, 0, 16, 0);
        }

        private void MainVerticalScrollBarCollapsed(object sender, MyScrollBarEventArgs e)
        {
            mainScrollViewer.FindDescendantOfType<StackPanel>().Margin = new Thickness(0, 0, 0, 0);
        }

        // Scrolls to end assuming a component was appended
        private void ScrollViewerLayoutUpdated(object sender, EventArgs e)
        {
            mainScrollViewer.ScrollToEnd();
            mainScrollViewer.LayoutUpdated -= ScrollViewerLayoutUpdated;
        }
    }
}