using System;

namespace BadBits.Engine.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class SpriteSheet
    {
        public string Texture { get; private set; }
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public int CellWidth { get; private set; }
        public int CellHeight { get; private set; }

        public SpriteSheet(string texture, int rows, int cols, int width, int height)
        {
            Texture = texture;
            Rows = rows;
            Cols = cols;
            CellWidth = width / cols;
            CellHeight = height / rows;
        }

    }
}
