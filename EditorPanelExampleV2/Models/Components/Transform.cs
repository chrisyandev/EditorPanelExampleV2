using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExampleV2.Models
{
    public class Transform
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Transform(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
