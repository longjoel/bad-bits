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

        public new Microsoft.Xna.Framework.Color Color => new Microsoft.Xna.Framework.Color
        {
            A = (byte)A.AsNumber(),
            R = (byte)R.AsNumber(),
            G = (byte)G.AsNumber(),
            B = (byte)B.AsNumber()
        };
    }
}
