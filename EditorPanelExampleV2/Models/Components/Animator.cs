using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExampleV2.Models
{
    public enum UpdateMode
    {
        Normal,
        AnimatePhysics,
        UnscaledTime
    }

    public enum CullingMode
    {
        AlwaysAnimate,
        CullUpdateTransforms,
        CullCompletely
    }

    public class Animator
    {
        public string Controller { get; set; }
        public string Avatar { get; set; }
        public bool ApplyRootMotion { get; set; }
        public UpdateMode CurrentUpdateMode { get; set; }
        public CullingMode CurrentCullingMode { get; set; }

        public Animator(string controller = "", string avatar = "", bool applyRootMotion = false,
            UpdateMode currentUpdateMode = UpdateMode.Normal,
            CullingMode currentCullingMode = CullingMode.AlwaysAnimate)
        {
            Controller = controller;
            Avatar = avatar;
            ApplyRootMotion = applyRootMotion;
            CurrentUpdateMode = currentUpdateMode;
            CurrentCullingMode = currentCullingMode;
        }
    }
}
