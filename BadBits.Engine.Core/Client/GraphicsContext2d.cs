using BadBits.Engine.Interfaces.Client;
using BadBits.Engine.Interfaces.Services;
using BadBits.Engine.Models.Host;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BadBits.Engine.Client
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

        private Dictionary<char, Point> _fontCache;

        void BuildFontCache()
        {
            _fontCache = new Dictionary<char, Point>();
            var data = new string[] {
                " !\"#$%&'()*+,-./0123456789:;<=>?",
                "@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_",
                "`abcdefghijklmnopqrstuvwxyz{|}~" };

            for (int i = 0; i < data.Length; i++)
            {
                var chrArray = data[i].ToCharArray();
                for (int j = 0; j < chrArray.Length; j++)
                {
                    var chr = chrArray[j];
                    _fontCache[chr] = new Point
                    {
                        X = j * 8,
                        Y = i * 16
                    };

                }
            }

        }

        void DrawText(int x, int y, string text, string textureName)
        {
            int row = 0;
            int col = 0;

            var chrArray = text.ToCharArray();

            foreach (var c in chrArray)
            {
                if (c == '\n')
                {
                    row++;
                    col = 0;
                }
                else
                {
                    var srcRect = new Rectangle(_fontCache[c], new Point(8, 16));
                    DrawCommands.Add(new DrawCommand2d
                    {
                        TextureName = textureName,
                        Source = srcRect,
                        Dest = new Rectangle { X = x + (col * 8), Y = y + (row * 16), Width = 8, Height = 16 }
                    });
                    col++;
                }
            }

        }

        public void drawLightText(int x, int y, string text)
        {
            if (_fontCache == null)
            {
                BuildFontCache();

            }
            if (!_resourceManager.TextureCache.ContainsKey("__light-font"))
            {
                _resourceManager.LoadTextureFromResource("__light-font", "lightFnt");
            }

            DrawText(x, y, text, "__light-font");
        }

        public void drawDarkText(int x, int y, string text)
        {
            if (_fontCache == null)
            {
                BuildFontCache();

            }
            if (!_resourceManager.TextureCache.ContainsKey("__dark-font"))
            {

                _resourceManager.LoadTextureFromResource("__dark-font", "darkFnt");
            }

            DrawText(x, y, text, "__dark-font");
        }
    }
}
