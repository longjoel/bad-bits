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
        void CreateShadedObject(string name, List<VertexPositionColor> vertexData);
        void CreateTexturedObject(string name, string textureName, List<VertexPositionTexture> vertexData);


    }
}
