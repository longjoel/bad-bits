using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Next.Models.Client
{
    public class VecPositionColor
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }

        public Microsoft.Xna.Framework.Graphics.VertexPositionColor VertexPositionColor => new Microsoft.Xna.Framework.Graphics.VertexPositionColor(
            new Microsoft.Xna.Framework.Vector3((float)X, 
                (float)Y, 
                (float)Z), new Microsoft.Xna.Framework.Color(new Microsoft.Xna.Framework.Vector3(
                    (float)R/255, 
                    (float)G/255, 
                    (float)B/255)));
    }
}
