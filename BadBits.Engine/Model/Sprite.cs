using System;

namespace BadBits.Engine.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Sprite
    {
        public Texture Texture { get; private set; }
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public int CellWidth { get; private set; }
        public int CellHeight { get; private set; }

        public Sprite(Texture texture, int rows, int cols)
        {
            Texture = texture;
            Rows = rows;
            Cols = cols;
            CellWidth = Texture.Width / cols;
            CellHeight = Texture.Height / rows;
        }

    }
}
