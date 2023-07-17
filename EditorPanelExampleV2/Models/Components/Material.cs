using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExampleV2.Models
{
    public class Material : ComponentModelBase
    {
        public Material(string name = "")
        {
            Name = name;
        }

        [Reactive]
        public string Name { get; set; }
    }
}
