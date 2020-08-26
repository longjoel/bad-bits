using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Next.Models.Client
{
    public class VecPositionColor
    {
        public Jint.Native.JsValue X { get; set; }
        public Jint.Native.JsValue Y { get; set; }
        public Jint.Native.JsValue Z { get; set; }

        public Jint.Native.JsValue R { get; set; }
        public Jint.Native.JsValue G { get; set; }
        public Jint.Native.JsValue B { get; set; }

        public Microsoft.Xna.Framework.Graphics.VertexPositionColor VertexPositionColor => new Microsoft.Xna.Framework.Graphics.VertexPositionColor(
            new Microsoft.Xna.Framework.Vector3((float)X.AsNumber(), 
                (float)Y.AsNumber(), 
                (float)Z.AsNumber()), new Microsoft.Xna.Framework.Color(new Microsoft.Xna.Framework.Vector3(
                    (float)R.AsNumber()/255, 
                    (float)G.AsNumber()/255, 
                    (float)B.AsNumber()/255)));
    }
}
