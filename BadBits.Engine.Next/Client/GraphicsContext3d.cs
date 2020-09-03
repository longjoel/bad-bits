using BadBits.Engine.Next.Interfaces.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Next.Client
{
    public class GraphicsContext3d : IGraphicsContext3d
    {
        public GraphicsContext3d()
        {
            TrianglesByColor = new Dictionary<Color, List<VertexPosition>>();
            TrianglesByTexture = new Dictionary<Texture2D, List<VertexPositionTexture>>();
        }

        public Dictionary<Color, List<VertexPosition>> TrianglesByColor { get; private set; }

        public Dictionary<Texture2D, List<VertexPositionTexture>> TrianglesByTexture { get; private set; } 

        public Matrix ViewMatrix { get; private set; }

    public Matrix WorldMatrix { get; private set; }

        public void drawColoredTriangles(object color, object[] verticies)
        {
            throw new NotImplementedException();
        }

        public void drawTexturedTriangles(string textureName, object[] verticices)
        {
            throw new NotImplementedException();
        }

        public void setView(double xEye, double yEye, double zEye, double xLook, double yLook, double zLook, double fov)
        {
            throw new NotImplementedException();
        }
    }
}
