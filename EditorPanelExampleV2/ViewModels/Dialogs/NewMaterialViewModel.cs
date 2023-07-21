using EditorPanelExampleV2.Models;
using EditorPanelExampleV2.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExampleV2.ViewModels
{
    public class NewMaterialViewModel : ViewModelBase
    {
        private string _newMaterial;
        private List<string> _invalidInputMessage;

        public NewMaterialViewModel()
        {
            AddCommand = ReactiveCommand.Create(() =>
            {
                return NewMaterial;
            });

            _invalidInputMessage = new List<string>();
        }

        public string NewMaterial
        {
            get => _newMaterial;
            set
            {
                if (value == _newMaterial) { return; }
                _newMaterial = value;
                this.RaisePropertyChanged(nameof(NewMaterial));

                Debug.WriteLine(_newMaterial);
            }
        }

        public List<string> InvalidInputMessage
        {
            get => _invalidInputMessage;
            set => this.RaiseAndSetIfChanged(ref _invalidInputMessage, value);
        }

        public ReactiveCommand<Unit, string> AddCommand { get; }
    }
}
