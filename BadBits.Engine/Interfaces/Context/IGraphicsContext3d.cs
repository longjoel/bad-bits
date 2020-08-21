using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BadBits.Engine.Interfaces.Context
{
    public interface IGraphicsContext3d
    {
        void DrawFlatShadedTriangle(Color c, List<VertexPosition> verticies);
        void DrawTexturedTriangle(string textureName, List<VertexPositionTexture> vertices);
        void DrawParticle(string textureName, VertexPosition location, double scale);
        void Render();
    }
}
