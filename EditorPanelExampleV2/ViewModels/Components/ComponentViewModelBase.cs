﻿using ReactiveUI;
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
        public ComponentViewModelBase()
        {

        }

        [Reactive]
        public string Title { get; set; } = "<ComponentTitle>";

        [Reactive]
        public bool IsCollapsed { get; set; }

        public List<string> ContextMenuItems { get; } = new()
        {
            "Remove Component",
            "Move Up",
            "Move Down"
        };
        
        public ReactiveCommand<Unit, Unit>? ToggleCollapseCommand { get; }

        public ReactiveCommand<Unit, Unit>? ContextMenuSelectedCommand { get; }
    }
}