using Avalonia.Controls;
using Avalonia.Input;
using System.Threading.Tasks;

namespace EditorPanelExampleV2.Services
{
    public interface IDragService
    {
        public Task StartDrag(object data, PointerEventArgs e, Control control);
        public void HandleDragEnter(object data, DragEventArgs e, Control control);
        public void HandleDrop(object data, DragEventArgs e, Control control);
    }
}
