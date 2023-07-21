using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EditorPanelExampleV2.Models;
using ReactiveUI;

namespace EditorPanelExampleV2.ViewModels
{
    public class TransformViewModel : ComponentViewModelBase
    {
        private Transform _transform;

        public TransformViewModel()
        {
            _transform = new Transform();
            SetupComponent();
        }

        public TransformViewModel(Transform transform)
        {
            _transform = transform;
            SetupComponent();
        }

        public float X
        {
            get => _transform.X;
            set
            {
                if (value == _transform.X) { return; }
                _transform.X = value;
                this.RaisePropertyChanged(nameof(X));

                Debug.WriteLine(_transform.X);
            }
        }

        public float Y
        {
            get => _transform.Y;
            set
            {
                if (value == _transform.Y) { return; }
                _transform.Y = value;
                this.RaisePropertyChanged(nameof(Y));

                Debug.WriteLine(_transform.Y);
            }
        }

        public float Z
        {
            get => _transform.Z;
            set
            {
                if (value == _transform.Z) { return; }
                _transform.Z = value;
                this.RaisePropertyChanged(nameof(Z));

                Debug.WriteLine(_transform.Z);
            }
        }

        private void SetupComponent()
        {
            Title = "Transform";
        }

        public void ClearTransform()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }
    }
}
