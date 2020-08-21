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
        private BasicEffect _texturedEffect;

        private readonly RenderTarget2D _renderTarget;

        private VertexBuffer _vertexBuffer;

        private Model.Texture _texture;



        public GraphicsContex3d(GraphicsDevice device, Interfaces.Context.IGraphicsContext2d context2d)
        {
            _graphics = device;
            _context2d = context2d;

            _flatShadedTriangles = new Dictionary<Color, List<VertexPosition>>();
            _texturedTriangles = new Dictionary<Texture2D, List<VertexPositionTexture>>();
            _particles = new Dictionary<Texture2D, List<VertexPosition>>();

            _flatShadedEffect = new BasicEffect(device)
            {
                FogEnabled = true,
                FogColor = new Vector3(),
                FogStart = 500,
                FogEnd = 512,
                LightingEnabled = false,
                Projection = Matrix.CreateOrthographicOffCenter(-512, 512, -512, 512, -512, 512),
                TextureEnabled = false ,
                VertexColorEnabled = true
            };

            _texturedEffect = new BasicEffect(device)
            {
                FogEnabled = true,
                FogColor = new Vector3(),
                FogStart = 500,
                FogEnd = 512,
                LightingEnabled = false,
                Projection = Matrix.CreateOrthographicOffCenter(-512, 512, -512, 512, -512, 512),
                TextureEnabled = true
            };

            _renderTarget = new RenderTarget2D(_graphics, 320, 240);

            var drawBuffer = new VertexPositionColorTexture[] {
                // top triangle
                new VertexPositionColorTexture{ Position = new Vector3{X = 0, Y=0, Z=0},TextureCoordinate= new Vector2{X=0,Y=0 }, Color= new Color(0xFFFFFFFF) },
                new VertexPositionColorTexture{ Position = new Vector3{X = 320, Y=0, Z=0},TextureCoordinate= new Vector2{X=1,Y=0 }, Color= new Color(0xFFFFFFFF) },
                new VertexPositionColorTexture{ Position = new Vector3{X = 320, Y=240, Z=0},TextureCoordinate= new Vector2{X=1,Y=1 }, Color= new Color(0xFFFFFFFF) },

                // bottom triangle
                new VertexPositionColorTexture{ Position = new Vector3{X = 320, Y=240, Z=0},TextureCoordinate= new Vector2{X=1,Y=1 }, Color= new Color(0xFFFFFFFF) },
                new VertexPositionColorTexture{ Position = new Vector3{X = 0, Y=240, Z=0},TextureCoordinate= new Vector2{X=0,Y=1 }, Color= new Color(0xFFFFFFFF) } ,
                new VertexPositionColorTexture{ Position = new Vector3{X = 0, Y=0, Z=0},TextureCoordinate= new Vector2{X=0,Y=0 }, Color= new Color(0xFFFFFFFF) }
            };

            _vertexBuffer.SetData(drawBuffer);

            _texture = new Model.Texture(_graphics, 320, 240);

        }

        public void DrawParticle(string textureName, VertexPosition location, double scale)
        {
            throw new NotImplementedException();
        }

        public void DrawFlatShadedTriangle(Color c, List<VertexPosition> verticies)
        {
            if (!_flatShadedTriangles.ContainsKey(c)) {
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
            _graphics.Clear(ClearOptions.DepthBuffer, Color.Black, 1024, 0);

            _graphics.BlendState = BlendState.Opaque;
          
            foreach (var t in _flatShadedEffect.Techniques)
            {
                foreach (var p in t.Passes)
                {
                    foreach (var kvp in _flatShadedTriangles)
                    {
                        var color = kvp.Key;

                        _graphics.DrawUserPrimitives(PrimitiveType.TriangleList, kvp.Value.Select(v => new VertexPositionColor(v.Position, color)).ToArray(), 0, kvp.Value.Count / 3);
                    }
                }
            }

            // ...

            _graphics.SetRenderTarget(null);

            _renderTarget.GetData(_texture.Data);

            _texture.SetData();

            _graphics.Clear(ClearOptions.DepthBuffer, Color.Transparent, _graphics.Viewport.MaxDepth, 0);

            _graphics.SetVertexBuffer(_vertexBuffer);

            _graphics.SamplerStates[0] = SamplerState.PointWrap;
            _graphics.SamplerStates[1] = SamplerState.PointWrap;
            _graphics.SamplerStates[2] = SamplerState.PointWrap;

            _graphics.BlendState = BlendState.AlphaBlend;


            using (var effect = new BasicEffect(_graphics))
            {
                effect.TextureEnabled = true;
                effect.Texture = _texture.Texture2D;

                effect.View = Matrix.CreateOrthographicOffCenter(new Rectangle(0, 0, 320, 240), -1, 1);
                effect.World = Matrix.Identity;

                effect.VertexColorEnabled = true;

                foreach (var pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    _graphics.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
                }
            }


        }
    }
}
