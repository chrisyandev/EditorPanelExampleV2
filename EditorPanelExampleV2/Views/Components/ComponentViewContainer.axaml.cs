using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using EditorPanelExampleV2.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace EditorPanelExampleV2.Views
{
    public partial class ComponentViewContainer : UserControl
    {
        public ComponentViewContainer()
        {
            InitializeComponent();
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            Border dragBorder = e.NameScope.Find<Border>("dragBorder")!;
            IComponentDragService cmptDragService = App.Current?.Services?.GetService<IComponentDragService>()!;

            AddHandler(DragDrop.DragEnterEvent, (sender, e) => cmptDragService?.HandleDragEnter(dragBorder, e, this));
            AddHandler(DragDrop.DropEvent, (sender, e) => cmptDragService?.HandleDrop(dragBorder, e, this));
        }
    }
}
