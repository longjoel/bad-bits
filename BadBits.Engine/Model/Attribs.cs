using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Model
{
    public class TextureAttribs
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class SpriteAttribs
    {
        public int rows { get; set; }
        public int cols { get; set; }
        public int cellWidth { get; set; }
        public int cellHeight { get; set; }
    }

}
