using BadBits.Engine.Models.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Interfaces.Client
{
    public interface IGraphicsContext2d
    {
        List<DrawCommand2d> DrawCommands { get; }

        void drawTexture(string texture, object srcRect, object destRect);
        void drawSprite(string spriteName, int x, int y, double frameTime);

        void drawLightText(int x, int y, string text);

        void drawDarkText(int x, int y, string text);
    }
}
