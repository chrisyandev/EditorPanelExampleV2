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
    public class LightViewModel : ComponentViewModelBase
    {
        private Light _light;

        public LightViewModel()
        {
            _light = new Light();
            SetupComponent();
        }

        public LightViewModel(Light light)
        {
            _light = light;
            SetupComponent();
        }

        #region Common Properties
        public List<string> Types { get; set; }

        public string SelectedType
        {
            get => _light.CurrentType;
            set
            {
                _light.CurrentType = value;
                this.RaisePropertyChanged(nameof(SelectedType));

                Debug.WriteLine(_light.CurrentType);

                // Update controls that depend on this property
                this.RaisePropertyChanged(nameof(Range));
                this.RaisePropertyChanged(nameof(IsRangeVisible));
                this.RaisePropertyChanged(nameof(IsSpotAngleVisible));
            }
        }

        public float Intensity
        {
            get => _light.Intensity;
            set
            {
                if (value == _light.Intensity) { return; }
                _light.Intensity = value;
                this.RaisePropertyChanged(nameof(Intensity));

                Debug.WriteLine(_light.Intensity);
            }
        }

        public List<string> ShadowTypes { get; set; }

        public string SelectedShadowType
        {
            get => _light.CurrentShadowType;
            set
            {
                _light.CurrentShadowType = value;
                this.RaisePropertyChanged(nameof(SelectedShadowType));

                Debug.WriteLine(_light.CurrentShadowType);

                // Update controls that depend on this property
                this.RaisePropertyChanged(nameof(IsShadowPropertiesVisible));
            }
        }
        #endregion

        #region Spot Light, Point Light Properties
        public float Range
        {
            get
            {
                if (_light.CurrentType == Light.Type.Spot)
                {
                    return _light.SpotLight.Range;
                }
                else if (_light.CurrentType == Light.Type.Point)
                {
                    return _light.PointLight.Range;
                }
                return Light.MIN_RANGE;
            }
            set
            {
                if (_light.CurrentType == Light.Type.Spot)
                {
                    if (value == _light.SpotLight.Range) { return; }
                    _light.SpotLight.Range = value;
                    this.RaisePropertyChanged(nameof(Range));

                    Debug.WriteLine(_light.SpotLight.Range);
                }
                else if (_light.CurrentType == Light.Type.Point)
                {
                    if (value == _light.PointLight.Range) { return; }
                    _light.PointLight.Range = value;
                    this.RaisePropertyChanged(nameof(Range));

                    Debug.WriteLine(_light.PointLight.Range);
                }
            }
        }

        public bool IsRangeVisible
        {
            get => SelectedType == Light.Type.Spot || SelectedType == Light.Type.Point;
        }
        #endregion

        #region Spot Light Properties
        public float SpotAngle
        {
            get => _light.SpotLight.SpotAngle;
            set
            {
                if (value == _light.SpotLight.SpotAngle) { return; }
                _light.SpotLight.SpotAngle = value;
                this.RaisePropertyChanged(nameof(SpotAngle));

                Debug.WriteLine(_light.SpotLight.SpotAngle);
            }
        }

        public bool IsSpotAngleVisible
        {
            get => SelectedType == Light.Type.Spot;
        }
        #endregion

        #region Hard Shadows, Soft Shadows Properties
        public float ShadowStrength
        {
            get => _light.Shadow.Strength;
            set
            {
                if (value == _light.Shadow.Strength) { return; }
                _light.Shadow.Strength = value;
                this.RaisePropertyChanged(nameof(ShadowStrength));

                Debug.WriteLine(_light.Shadow.Strength);
            }
        }

        public List<string> ShadowResolutions { get; set; }

        public string SelectedShadowResolution
        {
            get => _light.Shadow.CurrentResolution;
            set
            {
                _light.Shadow.CurrentResolution = value;
                this.RaisePropertyChanged(nameof(SelectedShadowResolution));

                Debug.WriteLine(_light.Shadow.CurrentResolution);
            }
        }

        public float ShadowBias
        {
            get => _light.Shadow.Bias;
            set
            {
                if (value == _light.Shadow.Bias) { return; }
                _light.Shadow.Bias = value;
                this.RaisePropertyChanged(nameof(ShadowBias));

                Debug.WriteLine(_light.Shadow.Bias);
            }
        }

        public bool IsShadowPropertiesVisible
        {
            get => SelectedShadowType != Light.ShadowType.NoShadows;
        }
        #endregion

        private void SetupComponent()
        {
            Title = "Light";

            Types = new List<string>(
                typeof(Light.Type).GetFields()
                .Select(field => field.GetValue(null) as string));

            ShadowTypes = new List<string>(
                typeof(Light.ShadowType).GetFields()
                .Select(field => field.GetValue(null) as string));

            ShadowResolutions = new List<string>(
                typeof(Light.ShadowClass.Resolution).GetFields()
                .Select(field => field.GetValue(null) as string));
        }
    }
}