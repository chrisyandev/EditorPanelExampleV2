using ReactiveUI;
using System.Reactive;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI.Fody.Helpers;

namespace EditorPanelExampleV2.ViewModels
{
    public class ComponentViewModelBase : ViewModelBase
    {
        [Reactive]
        public string Title { get; set; } = "<ComponentTitle>";

        [Reactive]
        public bool IsCollapsed { get; set; }

        public ICommand? ContextMenuSelectedCommand { get; set; }

        public void ToggleCollapse()
        {
            IsCollapsed = !IsCollapsed;
        }
    }
}
