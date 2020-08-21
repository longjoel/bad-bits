using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Model
{
    public class ObjectAttributes
    {
        public string objectName { get; set; }

        public bool visible { get; set; }

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public float roll { get; set; }
        public float pitch { get; set; }
        public float yaw { get; set; }

        public float scaleX { get; set; }
        public float scaleY { get; set; }
        public float scaleZ { get; set; }
    }
}
