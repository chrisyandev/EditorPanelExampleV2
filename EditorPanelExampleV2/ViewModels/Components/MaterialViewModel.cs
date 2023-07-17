using EditorPanelExampleV2.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExampleV2.ViewModels
{
    public class MaterialViewModel : ComponentViewModelBase
    {
        private Material _material;

        public MaterialViewModel()
        {
            _material = new Material();
        }

        public MaterialViewModel(Material material)
        {
            _material = material;
        }

        public string Material
        {
            get => _material.Name;
            set
            {
                if (value == _material.Name) { return; }
                _material.Name = value;

                Debug.WriteLine(_material.Name);
            }
        }
    }
}
