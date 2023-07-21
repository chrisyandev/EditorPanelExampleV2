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
            SetupComponent();
        }

        public MaterialViewModel(Material material)
        {
            _material = material;
            SetupComponent();
        }

        public string Material
        {
            get => _material.Name;
            set
            {
                if (value == _material.Name) { return; }
                _material.Name = value;
                this.RaisePropertyChanged(nameof(Material));

                Debug.WriteLine(_material.Name);
            }
        }

        private void SetupComponent()
        {
            Title = "Material";
        }

        public void ClearMaterial()
        {
            Material = "";
        }
    }
}
