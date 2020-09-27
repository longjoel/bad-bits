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

        public void drawColoredTriangles(object color, object[] verticies) {
            drawColoredTriangles(color, verticies, null);
        }
        public void drawColoredTriangles(object color, object[] verticies, object transform = null)
        {
            dynamic dynamicColor = color;
            var c = new Color((byte)dynamicColor.r, (byte)dynamicColor.g, (byte)dynamicColor.b);

            if (!TrianglesByColor.ContainsKey(c)) {
                TrianglesByColor[c] = new List<VertexPosition>();
            }

            if (transform == null)
            {
                var verts = verticies.Select(v =>
                {
                    dynamic dynamicVert = v;
                    return new VertexPosition(new Vector3((float)(dynamicVert.x), (float)dynamicVert.y, (float)dynamicVert.z));
                });

                TrianglesByColor[c].AddRange(verts);
            }
            else {
                dynamic t = transform;
                var sx = (float)t.scaleX;
                var sy = (float)t.scaleY;
                var sz = (float)t.scaleZ;

                var yaw = (float)t.yaw;
                var pitch = (float)t.pitch;
                var roll = (float)t.roll;

                var tx = (float)t.translateX;
                var ty = (float)t.translateY;
                var tz = (float)t.translateZ;

                var transformed = Matrix.CreateTranslation(tx, ty, tz) * Matrix.CreateFromYawPitchRoll(yaw, pitch, roll) * Matrix.CreateScale(sx, sy, sz);

                var verts = verticies.Select(v =>
                {
                    dynamic dynamicVert = v;
                    return new VertexPosition( Vector3.Transform( new Vector3((float)(dynamicVert.x), (float)dynamicVert.y, (float)dynamicVert.z),transformed));
                });

                TrianglesByColor[c].AddRange(verts);
            }
        }

        public void drawTexturedTriangles(string textureName, object[] verticies) {
            drawTexturedTriangles(textureName, verticies, null);
        }
        public void drawTexturedTriangles(string textureName, object[] verticies, object transform = null)
        {

            var tex2d = _resourceManager.TextureCache[textureName];

            if (!TrianglesByTexture.ContainsKey(tex2d)) {
                TrianglesByTexture[tex2d] = new List<VertexPositionTexture>();
            }

            if (transform == null)
            {

                var verts = verticies.Select(v =>
                {
                    dynamic dynamicVert = v;
                    return new VertexPositionTexture(new Vector3((float)dynamicVert.x, (float)dynamicVert.y, (float)dynamicVert.z), new Vector2((float)dynamicVert.u, (float)dynamicVert.v));
                });

                TrianglesByTexture[tex2d].AddRange(verts);
            }
            else {
                dynamic t = transform;
                var sx = (float)t.scaleX;
                var sy = (float)t.scaleY;
                var sz = (float)t.scaleZ;

                var yaw = (float)t.yaw;
                var pitch = (float)t.pitch;
                var roll = (float)t.roll;

                var tx = (float)t.translateX;
                var ty = (float)t.translateY;
                var tz = (float)t.translateZ;

                var transformed = Matrix.CreateTranslation(tx, ty, tz) * Matrix.CreateFromYawPitchRoll(yaw, pitch, roll) * Matrix.CreateScale(sx, sy, sz);

                var verts = verticies.Select(v =>
                {
                    dynamic dynamicVert = v;
                    return new VertexPositionTexture(Vector3.Transform(new Vector3((float)(dynamicVert.x), (float)dynamicVert.y, (float)dynamicVert.z), transformed),
                        new Vector2((float)dynamicVert.u, (float)dynamicVert.v));
                });

                TrianglesByTexture[tex2d].AddRange(verts);
            }
        }

        public void setView(double xEye, double yEye, double zEye, double xLook, double yLook, double zLook)
        {
            ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView((90f) * 0.0174533f, 320f / 240f, 0.001f, 100f);
            ViewMatrix = Matrix.CreateLookAt(new Vector3((float)xEye, (float)yEye, (float)zEye), new Vector3((float)xLook, (float)yLook, (float)zLook), new Vector3(0, 1, 0));
        }
    }
}
