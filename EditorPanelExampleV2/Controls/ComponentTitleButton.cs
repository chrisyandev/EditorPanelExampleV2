using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExampleV2.Controls
{
    public class ComponentTitleButton : Button
    {
        protected override Type StyleKeyOverride => typeof(Button);

        public event EventHandler<PointerPressedEventArgs>? LeftMouseButtonDown;
        public event EventHandler<PointerReleasedEventArgs>? LeftMouseButtonUp;

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
                LeftMouseButtonDown?.Invoke(this, e);
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            base.OnPointerReleased(e);

            if (e.InitialPressMouseButton == MouseButton.Left)
                LeftMouseButtonUp?.Invoke(this, e);
        }
    }
}
