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
        public Jint.Native.JsValue X { get; set; }
        public Jint.Native.JsValue Y { get; set; }
        public Jint.Native.JsValue Width { get; set; }
        public Jint.Native.JsValue Height { get; set; }

        public Rectangle BackendRect => new Rectangle {
            X = (int)X.AsNumber(),
            Y = (int)Y.AsNumber(),
            Width = (int)Width.AsNumber(),
            Height = (int)Height.AsNumber(),
        };
    }
}
