using System.Collections.Generic;

namespace BadBits.Engine.Model
{
    public class vertexPosition
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }

    public class TextureAttribs
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class SpriteCell
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class SpriteAttribs : Dictionary<string, SpriteCell>
    {

    }

}
