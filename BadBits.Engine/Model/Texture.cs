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

        private void ReorderPixels()
        {

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var a = Data[4 * (y * Width + x) + 0];
                    var r = Data[4 * (y * Width + x) + 1];
                    var g = Data[4 * (y * Width + x) + 2];
                    var b = Data[4 * (y * Width + x) + 3];


                    Data[4 * (y * Width + x) + 0] = r;
                    Data[4 * (y * Width + x) + 1] = g;
                    Data[4 * (y * Width + x) + 2] = b;
                    Data[4 * (y * Width + x) + 3] = a;
                }
            }
        }

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
            Texture2D = new Texture2D(g, width, height);

            IsDirty = true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="path"></param>
        public Texture(GraphicsDevice g, string path)
        {
            using (var img = System.Drawing.Image.FromFile(path))
            {

                Width = img.Width;
                Height = img.Height;

                Data = new byte[Width * Height * 4];
                Texture2D = new Texture2D(g, Width, Height);

                using (var bmp = new System.Drawing.Bitmap(img))
                {

                    var data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, Width, Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    System.Runtime.InteropServices.Marshal.Copy(data.Scan0, Data, 0, Width * Height * 4);

                    bmp.UnlockBits(data);

                }
            }
            ReorderPixels();
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

            Data[4 * (y * Width + x) + 0] = r;
            Data[4 * (y * Width + x) + 1] = g;
            Data[4 * (y * Width + x) + 2] = b;
            Data[4 * (y * Width + x) + 3] = a;

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
                    var rx = Data[4 * (y * Width + x) + 0];
                    var gx = Data[4 * (y * Width + x) + 1];
                    var bx = Data[4 * (y * Width + x) + 2];
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
