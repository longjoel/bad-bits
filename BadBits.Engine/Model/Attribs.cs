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

    public class SpriteCell {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class SpriteAttribs :Dictionary<string, SpriteCell>
    {
       
    }

}
