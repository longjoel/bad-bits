using Jint.Native;
using Jint.Native.Object;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Next.Models.Client
{
    public class Rect
    {
        public double x { get; set; }
        public double y { get; set; }
        public double width { get; set; }
        public double height { get; set; }

        public Rectangle BackendRect => new Rectangle {
            X = (int)x,
            Y = (int)y,
            Width = (int)width,
            Height = (int)height,
        };
    }
}
