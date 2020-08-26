using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BadBits.Engine.Next.Models.Host
{
    public class Texture : Texture2D
    {
        readonly byte[] _buffer;

        public bool IsDirty { get; private set; }

        public Texture(GraphicsDevice graphicsDevice, int width, int height) : base(graphicsDevice, width, height)
        {
            _buffer = new byte[Width * Height * 4];
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
    }
}
