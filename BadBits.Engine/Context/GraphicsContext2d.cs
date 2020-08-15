using BadBits.Engine.Interfaces.Context;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BadBits.Engine.Context
{

    class DrawCommand
    {
        public string TextureName { get; set; }
        public Rectangle SrcRect { get; set; }
        public Rectangle DestRect { get; set; }
    }

    public class GraphicsContext2d : IGraphicsContext2d
    {
        private GraphicsDevice _graphics;
        private Model.Texture _texture;
        private VertexBuffer _vertexBuffer;
        private SpriteBatch _spriteBatch;

        private readonly Dictionary<string, Model.Texture> _textureCache;
        private readonly Dictionary<string, Model.SpriteSheet> _spriteCache;

        private List<DrawCommand> _drawCommands;

        private RenderTarget2D _renderTarget;

        public GraphicsContext2d(GraphicsDevice graphics)
        {

            _graphics = graphics;
            _drawCommands = new List<DrawCommand>();
            _textureCache = new Dictionary<string, Model.Texture>();
            _spriteCache = new Dictionary<string, Model.SpriteSheet>();

            _texture = new Model.Texture(graphics, 320, 240);

            _vertexBuffer = new VertexBuffer(_graphics, typeof(VertexPositionColorTexture), 6, BufferUsage.None);

            var drawBuffer = new VertexPositionColorTexture[] {
                // top triangle
                new VertexPositionColorTexture{ Position = new Vector3{X = 5, Y=5, Z=0},TextureCoordinate= new Vector2{X=0,Y=0 }, Color= new Color(0xFFFFFFFF) },
                new VertexPositionColorTexture{ Position = new Vector3{X = 315, Y=5, Z=0},TextureCoordinate= new Vector2{X=1,Y=0 }, Color= new Color(0xFFFFFFFF) },
                new VertexPositionColorTexture{ Position = new Vector3{X = 315, Y=235, Z=0},TextureCoordinate= new Vector2{X=1,Y=1 }, Color= new Color(0xFFFFFFFF) },

                // bottom triangle
                new VertexPositionColorTexture{ Position = new Vector3{X = 315, Y=235, Z=0},TextureCoordinate= new Vector2{X=1,Y=1 }, Color= new Color(0xFFFFFFFF) },
                new VertexPositionColorTexture{ Position = new Vector3{X = 5, Y=235, Z=0},TextureCoordinate= new Vector2{X=0,Y=1 }, Color= new Color(0xFFFFFFFF) } ,
                new VertexPositionColorTexture{ Position = new Vector3{X = 5, Y=5, Z=0},TextureCoordinate= new Vector2{X=0,Y=0 }, Color= new Color(0xFFFFFFFF) }
            };

            _vertexBuffer.SetData(drawBuffer);

            _spriteBatch = new SpriteBatch(_graphics);

            _renderTarget = new RenderTarget2D(_graphics, 320, 240,false, SurfaceFormat.Color, DepthFormat.None);

        }

        public void CreateTexture(string name, int width, int height)
        {
            _textureCache[name] = new Model.Texture(_graphics, width, height);
        }

        public void SetSpriteSheet(string name, int rows, int cols) {
            _spriteCache[name] = new Model.SpriteSheet(name, rows, cols, _textureCache[name].Width, _textureCache[name].Height);
        }

        public void LoadTexture(string name, string path)
        {
            _textureCache[name] = new Model.Texture(_graphics, path);
        }

        public void LoadSpriteSheet(string name, string path, int rows, int cols) {
            LoadTexture(name, path);
            SetSpriteSheet(name, rows, cols);
        }

        public void SetPixel(string textureName, int x, int y, byte r, byte g, byte b, byte a)
        {
            _textureCache[textureName].SetPixel(x, y, r, g, b, a);
        }

        public void SetPixel(string textureName, int x, int y, byte r, byte g, byte b)
        {
            _textureCache[textureName].SetPixel(x, y, r, g, b);
        }

        public void Render()
        {

            foreach (var t in _textureCache.Values.Where(x => x.IsDirty)) {
                t.SetData();
            }

            _graphics.SetRenderTarget(_renderTarget);

            _graphics.Clear(Color.White);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap);

            foreach (var command in _drawCommands)
            {
                _spriteBatch.Draw(_textureCache[command.TextureName].Texture2D, command.DestRect, command.SrcRect, Color.White);
            }

            _spriteBatch.End();

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

            _drawCommands.Clear();

        }

        public void DrawSprite(string name, int x, int y, int row, int col)
        {
            var spriteSheet = _spriteCache[name];

            _drawCommands.Add(new DrawCommand
            {
                TextureName = spriteSheet.Texture,
                SrcRect = new Rectangle { X = col * spriteSheet.CellWidth, Y = row * spriteSheet.CellHeight, Width = spriteSheet.CellWidth, Height = spriteSheet.CellHeight },
                DestRect = new Rectangle { X = x, Y = y, Width = spriteSheet.CellWidth, Height = spriteSheet.CellHeight }
            });
        }

        public void DrawTexture(string name, int[] srcRect, int[] destRect)
        {
            _drawCommands.Add(new DrawCommand
            {
                TextureName = name,
                SrcRect = new Rectangle { X = srcRect[0], Y = srcRect[1], Width = srcRect[2], Height = srcRect[3] },
                DestRect = new Rectangle { X = destRect[0], Y = destRect[1], Width = destRect[2], Height = destRect[3] }
            });
        }
        
    }
}
