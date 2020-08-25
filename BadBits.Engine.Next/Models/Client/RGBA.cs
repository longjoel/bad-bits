using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Next.Models.Client
{
    public class Rgba : Rgb
    {
        public Jint.Native.JsValue A { get; set; }

        public new Backend.Color Color => new Backend.Color {
            A = (byte)A.AsNumber(),
            R = (byte)R.AsNumber(),
            G = (byte)G.AsNumber(),
            B = (byte)B.AsNumber()
        };
    }
}
