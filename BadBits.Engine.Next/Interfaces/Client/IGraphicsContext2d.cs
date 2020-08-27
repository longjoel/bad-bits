using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Next.Interfaces.Client
{
    public interface IGraphicsContext2d
    {
        void drawTexture(string texture, dynamic srcRect, dynamic dstRect);
        void drawSprite(string spriteName, int x, int y, double frameTime);
    }
}
