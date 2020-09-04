using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BadBits.Engine.Models.Host
{
    public class Texture : Texture2D
    {
        readonly Color[] _buffer;

        public bool IsDirty { get; private set; }

        public Texture(GraphicsDevice graphicsDevice, int width, int height) : base(graphicsDevice, width, height)
        {
            _buffer = new Color[Width * Height];
            IsDirty = true;
        }

        public void Update()
        {
            SetData(_buffer);
        }

        public Vector2[] GetVerticies(Rectangle srcRect)
        {
            return new Vector2[] {
                new Vector2(srcRect.X/Width, srcRect.Y/Height),
                new Vector2((srcRect.X+srcRect.Width)/Width, srcRect.Y/Height),
                new Vector2((srcRect.X+srcRect.Width)/Width, (srcRect.Y+srcRect.Height)/Height),

                new Vector2(srcRect.X/Width, srcRect.Y/Height),
                new Vector2((srcRect.X+srcRect.Width)/Width, (srcRect.Y+srcRect.Height)/Height),
                new Vector2(srcRect.X/Width, (srcRect.Y+srcRect.Height)/Height)
            };
        }

        public void SetPixel(int x, int y, Color c)
        {
            _buffer[y * Width + x] = c;
            IsDirty = true;
        }

        public void MakeTransparent(Color key)
        {
            for (int i = 0; i < Width * Height; i++)
            {
                _buffer[i].A = (_buffer[i] == key) ? (byte)0 : (byte)255;
            }
            IsDirty = true;
        }
    }
}
