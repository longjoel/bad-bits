using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Models.Host.Sprite
{
    public class Sprite:List<SpriteFrame>
    {

        public Sprite() : base() { }

        public SpriteFrame Interpolate(double dt) {

            var timeSum = 0.0;
            var index = 0;
            while (timeSum <= dt) {
                timeSum += this[index].FrameDurration;
                index++;
            }
            return this[index];
        }

    }
}
