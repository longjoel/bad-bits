using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Next.Models.Client
{
    public class Rgb
    {
        public Jint.Native.JsValue R { get; set; }
        public Jint.Native.JsValue G { get; set; }
        public Jint.Native.JsValue B { get; set; }

        public Microsoft.Xna.Framework.Color Color => new Microsoft.Xna.Framework.Color {
            A = (255),
            R = (byte)R.AsNumber(),
            G = (byte)G.AsNumber(),
            B = (byte)B.AsNumber()
        };
    }
}
