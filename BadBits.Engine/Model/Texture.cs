using Microsoft.Xna.Framework.Graphics;

namespace BadBits.Engine.Model
{
    public enum TextureDrawEnum
    {
        PreserveAlpha,
        OverwriteAlpha
    }

    /// <summary>
    /// 
    /// </summary>
    public class Texture
    {
        /// <summary>
        /// 
        /// </summary>
        public Texture2D Texture2D { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Data { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDirty { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Texture(GraphicsDevice g, int width, int height)
        {

            Width = width;
            Height = height;

            Data = new byte[width * height * 4];
            Texture2D = new Texture2D(g, width, height, false, SurfaceFormat.Color);

            IsDirty = true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="path"></param>
        public Texture(GraphicsDevice g, string path)
        {
            using (var img = System.Drawing.Image.FromFile(System.IO.Path.GetFullPath(path)))
            {

                Width = img.Width;
                Height = img.Height;

                Data = new byte[Width * Height * 4];
                Texture2D = new Texture2D(g, Width, Height, false, SurfaceFormat.Color);

                using (var bmp = new System.Drawing.Bitmap(img))
                {

                    for (int y = 0; y < Height; y++)
                    {
                        for (int x = 0; x < Width; x++)
                        {
                            var c = bmp.GetPixel(x, y);
                            SetPixel(x, y, c.R, c.G, c.B, c.A);
                        }
                    }

                }
            }

            SetData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public void SetPixel(int x, int y, byte r, byte g, byte b, byte a)
        {
            IsDirty = true;

            Data[(y * Width * 4) + (x * 4) + 0] = r;
            Data[(y * Width * 4) + (x * 4) + 1] = g;
            Data[(y * Width * 4) + (x * 4) + 2] = b;
            Data[(y * Width * 4) + (x * 4) + 3] = a;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public void SetPixel(int x, int y, byte r, byte g, byte b)
        {
            SetPixel(x, y, r, g, b, 255);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public void MakeTransparent(byte r, byte g, byte b)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var rx = Data[(y * Width * 4) + (x * 4) + 0];
                    var gx = Data[(y * Width * 4) + (x * 4) + 1];
                    var bx = Data[(y * Width * 4) + (x * 4) + 2];
                    var a = (byte)255;
                    if (rx == r && gx == g && bx == b)
                    {
                        a = 0;
                    }
                    SetPixel(x, y, rx, gx, bx, a);
                }
            }
            SetData();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetData()
        {
            Texture2D.SetData(Data);
            IsDirty = false;
        }

    }
}
