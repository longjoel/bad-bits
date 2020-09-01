using BadBits.Engine.Next.Interfaces.Client;
using BadBits.Engine.Next.Interfaces.Services;
using BadBits.Engine.Next.Models.Host;
using System.Collections.Generic;

namespace BadBits.Engine.Next.Client
{
    public class GraphicsContext2d : IGraphicsContext2d
    {
        private IResourceManager _resourceManager;

        public List<DrawCommand2d> DrawCommands { get; private set; }

        public GraphicsContext2d(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
            DrawCommands = new List<DrawCommand2d>();
        }

        public void drawSprite(string spriteName, int x, int y, double frameTime)
        {
            var sprite = _resourceManager.SpriteCache[spriteName];
            var frame = sprite.Interpolate(frameTime);

            DrawCommands.Add(new DrawCommand2d
            {
                TextureName = frame.TextureName,
                Dest = new Microsoft.Xna.Framework.Rectangle(
                    x,
                    y,
                    frame.SrcRectangle.Width,
                    frame.SrcRectangle.Height),
                Source = frame.SrcRectangle
            });
        }

        public void drawTexture(string texture, object srcRect, object destRect)
        {
            dynamic s = srcRect;
            dynamic d = destRect;

            DrawCommands.Add(new DrawCommand2d
            {
                TextureName = texture,
                Source = new Microsoft.Xna.Framework.Rectangle((int)s.x, (int)s.y, (int)s.width, (int)s.height),
                Dest = new Microsoft.Xna.Framework.Rectangle((int)d.x, (int)d.y, (int)d.width, (int)d.height)
            });
        }
    }
}
