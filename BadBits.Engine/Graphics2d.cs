using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BadBits.Engine
{
    public class Graphics2d : IGraphics2D
    {
        private readonly GraphicsDevice _graphics;

        private readonly byte[] _buffer;

        private Texture2D _texture2D;
        private VertexBuffer _vertexBuffer;

        public Graphics2d(GraphicsDevice graphics)
        {
            _graphics = graphics;
            _buffer = new byte[320 * 240 * 4];

            _texture2D = new Texture2D(graphics, 320, 240);
            _vertexBuffer = new VertexBuffer(_graphics, typeof(VertexPositionColorTexture), 6, BufferUsage.None);

            var drawBuffer = new VertexPositionColorTexture[] {
                // top triangle
                new VertexPositionColorTexture{ Position = new Vector3{X = 5, Y=5, Z=0},TextureCoordinate= new Vector2{X=0,Y=0 }, Color= new Color(0xFFFFFFFF) },
                new VertexPositionColorTexture{ Position = new Vector3{X = 315, Y=5, Z=0},TextureCoordinate= new Vector2{X=1,Y=0 }, Color= new Color(0xFFFFFFFF) },
                new VertexPositionColorTexture{ Position = new Vector3{X = 315, Y=235, Z=0},TextureCoordinate= new Vector2{X=1,Y=1 }, Color= new Color(0xFFFFFFFF) },

                // bottom triangle
                new VertexPositionColorTexture{ Position = new Vector3{X = 315, Y=235, Z=0},TextureCoordinate= new Vector2{X=1,Y=1 }, Color= new Color(0xFFFFFFFF) },
                new VertexPositionColorTexture{ Position = new Vector3{X = 5, Y=235, Z=0},TextureCoordinate= new Vector2{X=0,Y=1 }, Color= new Color(0xFFFFFFFF) } ,
                new VertexPositionColorTexture{ Position = new Vector3{X = 5, Y=5, Z=0},TextureCoordinate= new Vector2{X=0,Y=0 }, Color= new Color(0xFFFFFFFF) }
            };

            _vertexBuffer.SetData(drawBuffer);

        }

        public void clear()
        {
            Array.Clear(_buffer, 0, 320 * 240 * 4);
        }

        public void render()
        {

            _texture2D.SetData(_buffer);

            _graphics.SetVertexBuffer(_vertexBuffer);

            _graphics.SamplerStates[0] = SamplerState.PointClamp;
            _graphics.BlendState = BlendState.AlphaBlend;

            using (var effect = new BasicEffect(_graphics))
            {
                effect.TextureEnabled = true;
                effect.Texture = _texture2D;

                effect.View = Matrix.CreateOrthographicOffCenter(new Rectangle(0, 0, 320, 240), -1, 1);
                effect.World = Matrix.Identity;

                effect.VertexColorEnabled = true;

                foreach (var pass in effect.CurrentTechnique.Passes)
                {
                    
                    pass.Apply();
                    _graphics.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
                }
            }

        }



        public void setPixel(int x, int y, byte r, byte g, byte b, byte a)
        {

            _buffer[(y * 320 * 4) + (x * 4) + 0] = r;
            _buffer[(y * 320 * 4) + (x * 4) + 1] = g;
            _buffer[(y * 320 * 4) + (x * 4) + 2] = b;
            _buffer[(y * 320 * 4) + (x * 4) + 3] = a;
        }

        public void setRect(int x, int y, int w, int h, byte r, byte g, byte b, byte a)
        {
            for (int dy = y; dy < y + h; dy++)
            {
                for (int dx = x; dx < x + w; dx++)
                {
                    setPixel(dx, dy, r, g, b, a);
                }
            }
        }

    }
}
