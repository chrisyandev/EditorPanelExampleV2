using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using EditorPanelExampleV2.ViewModels;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace EditorPanelExampleV2.Services
{
    public class ComponentDragService : IComponentDragService
    {
        public ReactiveCommand<Tuple<ComponentViewModelBase, ComponentViewModelBase>, string>? GetDragDirectionCommand { get; set; }
        public ReactiveCommand<Tuple<ComponentViewModelBase, ComponentViewModelBase>, Unit>? InsertComponentCommand { get; set; }

        public async Task StartDrag(object dragBorder, PointerEventArgs e, Control control)
        {
            Debug.WriteLine("Drag Start");

            DataObject dragData = new DataObject();

            dragData.Set("SourceComponent", control.DataContext);
            // Use array for data that needs to be modified during drag
            dragData.Set("LastBorder", new Border[] { (Border)dragBorder });
            dragData.Set("DragDirection", new string[] { "none" });

            DragDropEffects result = await DragDrop.DoDragDrop(e, dragData, DragDropEffects.Move);

            if (result == DragDropEffects.None)
            {
                Border lastBorder = (dragData.Get("LastBorder") as Border[])[0];
                lastBorder.BorderThickness = new Thickness(0, 0, 0, 0);
            }
        }

        public void HandleDragEnter(object dragBorder, DragEventArgs e, Control control)
        {
            Border currentBorder = (Border)dragBorder;
            Border lastBorder = (e.Data.Get("LastBorder") as Border[])[0];

            if (currentBorder == lastBorder)
            {
                string currentDragDirection = (e.Data.Get("DragDirection") as string[])[0];
                PaintBorder(currentBorder, Brushes.LightBlue, currentDragDirection);
            }
            else
            {
                ComponentViewModelBase? targetComponent = control.DataContext as ComponentViewModelBase;
                ComponentViewModelBase? sourceComponent = e.Data.Get("SourceComponent") as ComponentViewModelBase;

                GetDragDirectionCommand?.Execute(Tuple.Create(targetComponent, sourceComponent)).Subscribe(dragDirection =>
                {
                    PaintBorder(currentBorder, Brushes.LightBlue, dragDirection);

                    lastBorder.BorderThickness = new Thickness(0, 0, 0, 0);
                    (e.Data.Get("LastBorder") as Border[])[0] = currentBorder;
                    (e.Data.Get("DragDirection") as string[])[0] = dragDirection;
                });
            }
        }

        public void HandleDrop(object data, DragEventArgs e, Control control)
        {
            Debug.WriteLine("Drag End");

            e.DragEffects = DragDropEffects.Move;

            ComponentViewModelBase targetComponent = control.DataContext as ComponentViewModelBase;
            Debug.WriteLine($"Target: {targetComponent}");

            ComponentViewModelBase sourceComponent = e.Data.Get("SourceComponent") as ComponentViewModelBase;
            Debug.WriteLine($"Source: {sourceComponent}");

            InsertComponentCommand?.Execute(Tuple.Create(targetComponent, sourceComponent)).Subscribe(); // does not work without Subscribe()

            Border lastBorder = (e.Data.Get("LastBorder") as Border[])[0];
            lastBorder.BorderThickness = new Thickness(0, 0, 0, 0);
        }

        private void PaintBorder(Border border, ISolidColorBrush brush, string dragDirection)
        {
            border.BorderBrush = brush;

            if (dragDirection == "up")
            {
                border.BorderThickness = new Thickness(0, 2, 0, 0);
            }
            else if (dragDirection == "down")
            {
                border.BorderThickness = new Thickness(0, 0, 0, 2);
            }
            else
            {
                border.BorderThickness = new Thickness(0, 2, 0, 0);
            }
        }
    }
}
