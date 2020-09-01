using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Next.Models.Client
{
    public class Rgba : Rgb
    {
        public double A { get; set; }

        public new Microsoft.Xna.Framework.Color Color => new Microsoft.Xna.Framework.Color
        {
            A = (byte)A,
            R = (byte)R,
            G = (byte)G,
            B = (byte)B,
        };
    }
}
