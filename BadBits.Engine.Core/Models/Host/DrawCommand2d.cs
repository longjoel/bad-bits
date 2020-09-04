using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Models.Host
{
    public class DrawCommand2d
    {
        public string TextureName { get; set; }
        public Rectangle Source { get; set; }
        public Rectangle Dest { get; set; }
    }
}
