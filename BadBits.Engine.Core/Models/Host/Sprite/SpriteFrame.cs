using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Models.Host.Sprite
{
    public class SpriteFrame
    {
        public Rectangle SrcRect { get; set; }
        public string TextureName { get; set; }
        public double FrameDurration { get; set; }
    }

}
