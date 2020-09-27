using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Interfaces.Client
{
    public interface IGraphicsContext3d
    {
        Dictionary<Color, List<VertexPosition>> TrianglesByColor { get; }
        Dictionary<Texture2D, List<VertexPositionTexture>> TrianglesByTexture { get; }
        Matrix ProjectionMatrix { get; }
        Matrix ViewMatrix { get; }

        void drawColoredTriangles(object color, object[] verticies, object transform = null);
        void drawColoredTriangles(object color, object[] verticies);
        void drawTexturedTriangles(string textureName, object[] verticies, object transform = null);
        void drawTexturedTriangles(string textureName, object[] verticies);

        void setView(double xEye, double yEye, double zEye, double xLook, double yLook, double zLook);
    }
}
