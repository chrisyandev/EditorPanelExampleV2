using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System;

namespace EditorPanelExampleV2.Controls
{
    public class MyScrollBarEventArgs : EventArgs
    {
        public MyScrollBarEventArgs(ScrollBar scrollBar)
        {
            ScrollBar = scrollBar;
        }

        public ScrollBar ScrollBar { get; set; }
    }

    public sealed class MainScrollViewer : ScrollViewer
    {
        protected override Type StyleKeyOverride => typeof(ScrollViewer);

        public EventHandler<MyScrollBarEventArgs> VerticalScrollBarExpanded;
        public EventHandler<MyScrollBarEventArgs> VerticalScrollBarCollapsed;

        private ScrollBar _verticalScrollBar;

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            _verticalScrollBar = e.NameScope.Find<ScrollBar>("PART_VerticalScrollBar");
            _verticalScrollBar.PropertyChanged += VerticalScrollBarPropertyChanged;
        }

        private void VerticalScrollBarPropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == IsExpandedProperty)
            {
                MyScrollBarEventArgs myArgs = new(_verticalScrollBar);

                if (IsExpanded == true)
                {
                    VerticalScrollBarExpanded?.Invoke(this, myArgs);
                }
                else
                {
                    VerticalScrollBarCollapsed?.Invoke(this, myArgs);
                }
            }
        }
    }
}
