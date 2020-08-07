
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
            _textureHandle = gl.GenTexture();

            gl.BindTexture(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, _textureHandle);

            gl.TexParameterI(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureMinFilter, new int[(int)OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest]);
            gl.TexParameterI(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, OpenTK.Graphics.OpenGL.TextureParameterName.TextureMagFilter, new int[(int)OpenTK.Graphics.OpenGL.TextureMinFilter.Nearest]);

            gl.TexImage2D(OpenTK.Graphics.OpenGL.TextureTarget.Texture2D, 0, OpenTK.Graphics.OpenGL.PixelInternalFormat.Rgba,
                512, 256, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, OpenTK.Graphics.OpenGL.PixelType.Byte, _buffer);

        }

        public void Render()
        {

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

            gl.Begin(OpenTK.Graphics.OpenGL.PrimitiveType.Quads);

            gl.End();

            gl.BlendFunc(OpenTK.Graphics.OpenGL.BlendingFactor.SrcAlpha, OpenTK.Graphics.OpenGL.BlendingFactor.OneMinusSrcAlpha);
        }
    }
}
