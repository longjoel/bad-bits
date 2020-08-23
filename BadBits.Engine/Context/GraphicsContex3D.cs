using BadBits.Engine.Interfaces.Context;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BadBits.Engine.Context
{


    public class GraphicsContex3d : Interfaces.Context.IGraphicsContext3d
    {
        private GraphicsDevice _graphics;
        private readonly IGraphicsContext2d _context2d;

        private readonly Dictionary<Color, List<VertexPosition>> _flatShadedTriangles;
        private readonly Dictionary<Texture2D, List<VertexPositionTexture>> _texturedTriangles;
        private readonly Dictionary<Texture2D, List<VertexPosition>> _particles;

        private BasicEffect _flatShadedEffect;
        private readonly BasicEffect _texturedEffect;

        private readonly RenderTarget2D _renderTarget;

        private VertexBuffer _vertexBuffer;

        private readonly Model.Texture _texture;



        public GraphicsContex3d(GraphicsDevice device, IGraphicsContext2d context2d, RenderTarget2D renderTarget)
        {
            _graphics = device;
            _context2d = context2d;

            _flatShadedTriangles = new Dictionary<Color, List<VertexPosition>>();
            _texturedTriangles = new Dictionary<Texture2D, List<VertexPositionTexture>>();
            _particles = new Dictionary<Texture2D, List<VertexPosition>>();
            _texture = new Model.Texture(_graphics, 320, 240);
            _vertexBuffer = new VertexBuffer(_graphics, typeof(VertexPositionColorTexture), 6, BufferUsage.None);


            _flatShadedEffect = new BasicEffect(device)
            {
                FogEnabled = true,
                FogColor = new Vector3(),
                FogStart = 90,
                FogEnd = 100,
                LightingEnabled = false,
                Projection = Matrix.CreateOrthographicOffCenter(0, 320, 240, 0, -100, 100),
                TextureEnabled = false,
                VertexColorEnabled = true
            };

            _texturedEffect = new BasicEffect(device)
            {
                FogEnabled = true,
                FogColor = new Vector3(),
                FogStart = 90,
                FogEnd = 100,
                LightingEnabled = false,
                Projection = Matrix.CreateOrthographicOffCenter(0, 320, 240, 0, -100, 100),
                TextureEnabled = true
            };

            _renderTarget = renderTarget;


        }

        public void DrawParticle(string textureName, VertexPosition location, double scale)
        {
            throw new NotImplementedException();
        }

        public void DrawFlatShadedTriangle(Color c, List<VertexPosition> verticies)
        {
            if (!_flatShadedTriangles.ContainsKey(c))
            {
                _flatShadedTriangles[c] = new List<VertexPosition>();
            }

            _flatShadedTriangles[c].AddRange(verticies);
        }

        public void DrawTexturedTriangle(string textureName, List<VertexPositionTexture> vertices)
        {
            throw new NotImplementedException();
        }

        public void Render()
        {
            _graphics.SetRenderTarget(_renderTarget);
            _graphics.Clear(Color.Transparent);
            _graphics.RasterizerState = RasterizerState.CullNone;

            foreach (var p in _flatShadedEffect.CurrentTechnique.Passes)
            {
                p.Apply();
                foreach (var kvp in _flatShadedTriangles)
                {
                    var color = kvp.Key;

                    _graphics.DrawUserPrimitives(PrimitiveType.TriangleList, 
                        kvp.Value.Select(v => new VertexPositionColor(v.Position, color)).ToArray(), 0, kvp.Value.Count / 3);
                }
            }

            _flatShadedTriangles.Clear();

            foreach (var p in _texturedEffect.CurrentTechnique.Passes)
            {
                p.Apply();
                foreach (var kvp in _texturedTriangles)
                {
                    var texture = kvp.Key;
                    _texturedEffect.Texture = texture;
                    _texturedEffect.TextureEnabled = true;

                    _graphics.DrawUserPrimitives(PrimitiveType.TriangleList,
                        kvp.Value.Select(v => new VertexPositionTexture(v.Position, v.TextureCoordinate)).ToArray(), 0, kvp.Value.Count / 3);
                }
            }

            _texturedTriangles.Clear();

            _graphics.SetRenderTarget(null);
        }
    }
}
