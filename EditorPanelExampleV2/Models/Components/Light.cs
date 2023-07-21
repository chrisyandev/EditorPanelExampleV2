using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExampleV2.Models
{
    public class Light
    {
        public const float MIN_INTENSITY = 0;
        public const float MIN_RANGE = 0;
        public const float SPOTLIGHT_MIN_SPOT_ANGLE = 1;
        public const float SPOTLIGHT_MAX_SPOT_ANGLE = 179;
        public const float SHADOW_MIN_STRENGTH = 0;
        public const float SHADOW_MAX_STRENGTH = 1;
        public const float SHADOW_MIN_BIAS = 0;
        public const float SHADOW_MAX_BIAS = 2;

        private float _intensity;
        private SpotLightClass _spotLight;
        private PointLightClass _pointLight;
        private ShadowClass _shadow;

        public static class Type
        {
            public static readonly string Spot = "Spot";
            public static readonly string Directional = "Directional";
            public static readonly string Point = "Point";
        }

        public static class ShadowType
        {
            public static readonly string NoShadows = "No Shadows";
            public static readonly string HardShadows = "Hard Shadows";
            public static readonly string SoftShadows = "Soft Shadows";
        }

        public Light()
        {
            CurrentType = Type.Spot;
            CurrentShadowType = ShadowType.NoShadows;
            Intensity = 1;
        }

        public Light(string type, string shadowType, float intensity)
        {
            CurrentType = type;
            CurrentShadowType = shadowType;
            Intensity = intensity;
        }

        public string CurrentType { get; set; }
        public string CurrentShadowType { get; set; }
        public float Intensity
        {
            get => _intensity;
            set => _intensity = value < MIN_INTENSITY ? MIN_INTENSITY : value;
        }

        public SpotLightClass SpotLight
        {
            get
            {
                _spotLight ??= new SpotLightClass();
                return _spotLight;
            }
        }

        public PointLightClass PointLight
        {
            get
            {
                _pointLight ??= new PointLightClass();
                return _pointLight;
            }
        }

        public ShadowClass Shadow
        {
            get
            {
                _shadow ??= new ShadowClass();
                return _shadow;
            }
        }

        #region Nested Classes
        public class SpotLightClass
        {
            private float _range;
            private float _spotAngle;

            public SpotLightClass()
            {
                Range = 10;
                SpotAngle = 30;
            }

            public float Range
            {
                get => _range;
                set => _range = value < MIN_RANGE ? MIN_RANGE : value;
            }

            public float SpotAngle
            {
                get => _spotAngle;
                set => _spotAngle = value < SPOTLIGHT_MIN_SPOT_ANGLE ? SPOTLIGHT_MIN_SPOT_ANGLE
                                : value > SPOTLIGHT_MAX_SPOT_ANGLE ? SPOTLIGHT_MAX_SPOT_ANGLE
                                : value;
            }
        }

        public class PointLightClass
        {
            private float _range;

            public PointLightClass()
            {
                Range = 10;
            }

            public float Range
            {
                get => _range;
                set => _range = value < MIN_RANGE ? MIN_RANGE : value;
            }
        }

        public class ShadowClass
        {
            private float _strength;
            private float _bias;

            public static class Resolution
            {
                public static readonly string Low = "Low";
                public static readonly string Medium = "Medium";
                public static readonly string High = "High";
            }

            public ShadowClass()
            {
                Strength = SHADOW_MAX_STRENGTH;
                CurrentResolution = Resolution.High;
                Bias = 0.05f;
            }

            public float Strength
            {
                get => _strength;
                set => _strength = value < SHADOW_MIN_STRENGTH ? SHADOW_MIN_STRENGTH
                                : value > SHADOW_MAX_STRENGTH ? SHADOW_MAX_STRENGTH
                                : value;
            }
            public string CurrentResolution { get; set; }
            public float Bias
            {
                get => _bias;
                set => _bias = value < SHADOW_MIN_BIAS ? SHADOW_MIN_BIAS
                            : value > SHADOW_MAX_BIAS ? SHADOW_MAX_BIAS
                            : value;
            }
        }
        #endregion
    }
}