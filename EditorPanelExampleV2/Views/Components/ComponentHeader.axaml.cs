using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using EditorPanelExampleV2.Services;
using System.Collections.Generic;
using System;
using System.Linq;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using Avalonia.Controls.Utils;
using Avalonia;

namespace EditorPanelExampleV2.Views
{
    public partial class ComponentHeader : UserControl
    {
        public ComponentHeader()
        {
            InitializeComponent();

            DragDrop.SetAllowDrop(this, true);

            var cmptDragService = App.Current?.Services?.GetServices<IDragService>()
                .FirstOrDefault(s => s.GetType() == typeof(ComponentDragService));
            
            componentTitleButton.LeftMouseButtonDown += async (sender, e) =>
            {
                if (cmptDragService != null)
                {
                    Border dragBorder = (Border)this.GetVisualAncestors().First(x => x.Name == "dragBorder");
                    await cmptDragService.StartDrag(dragBorder, e, this);
                }
            };
        }
    }
}
