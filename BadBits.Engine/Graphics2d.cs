
using gl = OpenTK.Graphics.OpenGL.GL;

namespace BadBits.Engine
{
    public class Graphics2D
    {
        private readonly byte[] _buffer;
        private readonly int _textureHandle;

        public Graphics2D()
        {
            _buffer = new byte[512 * 256 * 4];
            gl.GenTextures(1, out _textureHandle);

            gl.BindTexture(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, _textureHandle);

            gl.TexImage2D(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, 0, OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgba,
             512, 256, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, OpenTK.Graphics.OpenGL.PixelType.Byte, _buffer);

            gl.TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureMinFilter, (int)OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest);
            gl.TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureMagFilter, (int)OpenTK.Graphics.OpenGL.TextureMagFilter.Nearest);
            gl.TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureWrapS, (int)OpenTK.Graphics.OpenGL.TextureWrapMode.Repeat);
            gl.TexParameter(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureWrapT, (int)OpenTK.Graphics.OpenGL.TextureWrapMode.Repeat);


        }

        public void clear()
        {
            System.Array.Clear(_buffer, 0, 512 * 256 * 4);
        }

        public void setPixel(int x, int y, byte r, byte g, byte b, byte a)
        {

            _buffer[(y * 512) + ((x * 4) + 0)] = r;
            _buffer[(y * 512) + ((x * 4) + 1)] = g;
            _buffer[(y * 512) + ((x * 4) + 2)] = b;
            _buffer[(y * 512) + ((x * 4) + 3)] = a;
        }

        public void render()
        {
            gl.Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.DepthBufferBit);

            // update the texture.
            gl.BindTexture(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, _textureHandle);
            gl.TexSubImage2D(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, 0, 0, 0, 512, 256, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, OpenTK.Graphics.OpenGL.PixelType.Byte, _buffer);

            OpenTK.Matrix4 mtx;
            OpenTK.Matrix4.CreateOrthographicOffCenter(0, 320, 240, 0, 1, -1, out mtx);

            gl.LoadMatrix(ref mtx);

            gl.Enable(OpenTK.Graphics.OpenGL.EnableCap.Blend);
            gl.Enable(OpenTK.Graphics.OpenGL.EnableCap.Texture2D);

            gl.Disable(OpenTK.Graphics.OpenGL.EnableCap.DepthTest);
            gl.Disable(OpenTK.Graphics.OpenGL.EnableCap.CullFace);
            gl.Disable(OpenTK.Graphics.OpenGL.EnableCap.CullFace);

            gl.BlendFunc(OpenTK.Graphics.OpenGL.BlendingFactor.SrcAlpha,
                OpenTK.Graphics.OpenGL.BlendingFactor.OneMinusSrcAlpha);

            gl.AlphaFunc(OpenTK.Graphics.OpenGL.AlphaFunction.Gequal, 0.9f);

            gl.BindTexture(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, _textureHandle);

            gl.Begin(OpenTK.Graphics.OpenGL.PrimitiveType.Quads);

            gl.Color4(new byte[] { 255, 255, 255, 250 });

            gl.TexCoord2(0f, .9375f); gl.Vertex2(5f, 5f);

            gl.TexCoord2(.625f, .9375f); gl.Vertex2(315f, 5f);

            gl.TexCoord2(.625f, 0f); gl.Vertex2(315f, 235f);

            gl.TexCoord2(0f, 0f); gl.Vertex2(5f, 235f);

            gl.End();

        }
    }
}
