using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Next.Models.Client
{
    public class Rgb
    {
        public decimal R { get; set; }
        public decimal G { get; set; }
        public decimal B { get; set; }

        public Microsoft.Xna.Framework.Color Color => new Microsoft.Xna.Framework.Color {
            A = (255),
            R = (byte)R,
            G = (byte)G,
            B = (byte)B,
        };
    }
}
