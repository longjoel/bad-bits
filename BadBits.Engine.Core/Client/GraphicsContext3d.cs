using BadBits.Engine.Interfaces.Client;
using BadBits.Engine.Interfaces.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BadBits.Engine.Client
{
    public class GraphicsContext3d : IGraphicsContext3d
    {
        private IResourceManager _resourceManager;

        public GraphicsContext3d(IResourceManager resourceManager)
        {
            TrianglesByColor = new Dictionary<Color, List<VertexPosition>>();
            TrianglesByTexture = new Dictionary<Texture2D, List<VertexPositionTexture>>();
            _resourceManager = resourceManager;
            ProjectionMatrix = ProjectionMatrix = Matrix.CreateOrthographicOffCenter(-10, 10, -10, 10, -10, 10);
            ViewMatrix = Matrix.Identity;
        }

        public Dictionary<Color, List<VertexPosition>> TrianglesByColor { get; private set; }

        public Dictionary<Texture2D, List<VertexPositionTexture>> TrianglesByTexture { get; private set; }

        public Matrix ProjectionMatrix { get; private set; }

        public Matrix ViewMatrix { get; private set; }

        public void drawColoredTriangles(object color, object[] verticies)
        {
            dynamic dynamicColor = color;
            var c = new Color((byte)dynamicColor.r, (byte)dynamicColor.g, (byte)dynamicColor.b);

            if (!TrianglesByColor.ContainsKey(c)) {
                TrianglesByColor[c] = new List<VertexPosition>();
            }

            var verts = verticies.Select(v => {
                dynamic dynamicVert = v;
                return new VertexPosition(new Vector3((float)(dynamicVert.x), (float)dynamicVert.y, (float)dynamicVert.z));
            });

            TrianglesByColor[c].AddRange(verts);
        }

        public void drawTexturedTriangles(string textureName, object[] verticies)
        {

            var tex2d = _resourceManager.TextureCache[textureName];

            if (!TrianglesByTexture.ContainsKey(tex2d)) {
                TrianglesByTexture[tex2d] = new List<VertexPositionTexture>();
            }

            var verts = verticies.Select(v => {
                dynamic dynamicVert = v;
                return new VertexPositionTexture(new Vector3((float)dynamicVert.x, (float)dynamicVert.y, (float)dynamicVert.z), new Vector2((float)dynamicVert.u, (float)dynamicVert.v) );
            });

            TrianglesByTexture[tex2d].AddRange(verts);
        }

        public void setView(double xEye, double yEye, double zEye, double xLook, double yLook, double zLook)
        {
            ProjectionMatrix = Matrix.CreateOrthographicOffCenter(-10,10,-10,10,-10,10);
          ViewMatrix = Matrix.CreateLookAt(new Vector3((float)xEye, (float)yEye, (float)zEye), new Vector3((float)xLook, (float)yLook, (float)zLook), new Vector3(0, 1, 0));
        }
    }
}
