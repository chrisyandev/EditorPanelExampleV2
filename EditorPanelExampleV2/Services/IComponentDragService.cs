using Avalonia.Controls;
using Avalonia.Input;
using EditorPanelExampleV2.ViewModels;
using ReactiveUI;
using System.Reactive;
using System;
using System.Threading.Tasks;

namespace EditorPanelExampleV2.Services
{
    public interface IComponentDragService
    {
        public ReactiveCommand<Tuple<ComponentViewModelBase, ComponentViewModelBase>, string>? GetDragDirectionCommand { get; set; }
        public ReactiveCommand<Tuple<ComponentViewModelBase, ComponentViewModelBase>, Unit>? InsertComponentCommand { get; set; }

        public Task StartDrag(object data, PointerEventArgs e, Control control);
        public void HandleDragEnter(object data, DragEventArgs e, Control control);
        public void HandleDrop(object data, DragEventArgs e, Control control);
    }
}
