using BadBits.Engine.Interfaces.Context;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
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
        private Color _clearColor;

        public Dictionary<string, Model.Texture> TextureCache { get; private set; }
        public Dictionary<string, Model.SpriteSheet> SpriteCache { get; private set; }

        private List<DrawCommand> _drawCommands;

        private RenderTarget2D _renderTarget;

        public GraphicsContext2d(GraphicsDevice graphics, RenderTarget2D renderTarget)
        {
            TextureCache = new Dictionary<string, Model.Texture>();
            SpriteCache = new Dictionary<string, Model.SpriteSheet>();

            _graphics = graphics;
            _drawCommands = new List<DrawCommand>();

            _texture = new Model.Texture(graphics, 320, 240);
           

            _vertexBuffer = new VertexBuffer(_graphics, typeof(VertexPositionColorTexture), 6, BufferUsage.None);

            var drawBuffer = new VertexPositionColorTexture[] {
                // top triangle
                new VertexPositionColorTexture{ Position = new Vector3{X = 0, Y=0, Z=0.95f},TextureCoordinate= new Vector2{X=0,Y=0 }, Color= new Color(0xFFFFFFFF) },
                new VertexPositionColorTexture{ Position = new Vector3{X = 320, Y=0, Z=0.95f},TextureCoordinate= new Vector2{X=1,Y=0 }, Color= new Color(0xFFFFFFFF) },
                new VertexPositionColorTexture{ Position = new Vector3{X = 320, Y=240, Z=0.95f},TextureCoordinate= new Vector2{X=1,Y=1 }, Color= new Color(0xFFFFFFFF) },

                // bottom triangle
                new VertexPositionColorTexture{ Position = new Vector3{X = 320, Y=240,Z=0.95f},TextureCoordinate= new Vector2{X=1,Y=1 }, Color= new Color(0xFFFFFFFF) },
                new VertexPositionColorTexture{ Position = new Vector3{X = 0, Y=240, Z=0.95f},TextureCoordinate= new Vector2{X=0,Y=1 }, Color= new Color(0xFFFFFFFF) } ,
                new VertexPositionColorTexture{ Position = new Vector3{X = 0, Y=0,Z=0.95f},TextureCoordinate= new Vector2{X=0,Y=0 }, Color= new Color(0xFFFFFFFF) }
            };

            _vertexBuffer.SetData(drawBuffer);

            _spriteBatch = new SpriteBatch(_graphics);

            _renderTarget = renderTarget;

        }

        public void CreateTexture(string name, int width, int height)
        {
            TextureCache[name] = new Model.Texture(_graphics, width, height);
        }


        public void LoadTexture(string name, string path)
        {
            TextureCache[name] = new Model.Texture(_graphics, path);
        }

        public void LoadSpriteSheet(string name, string path, string spriteSheetPath)
        {
            LoadTexture(name, path);

            var spriteFrames = JsonConvert.DeserializeObject<Dictionary<string, Rectangle>>(System.IO.File.ReadAllText(spriteSheetPath));

            SpriteCache[name] = new Model.SpriteSheet(name) { Cells = spriteFrames };

        }

        public void SetPixel(string textureName, int x, int y, byte r, byte g, byte b, byte a)
        {
            TextureCache[textureName].SetPixel(x, y, r, g, b, a);
        }

        public void SetPixel(string textureName, int x, int y, byte r, byte g, byte b)
        {
            TextureCache[textureName].SetPixel(x, y, r, g, b);
        }

        public void Render()
        {

            foreach (var t in TextureCache.Values.Where(x => x.IsDirty))
            {
                t.SetData();
            }

            _graphics.SetRenderTarget(_renderTarget);

           // _graphics.Clear(Color.Transparent);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap);

            foreach (var command in _drawCommands)
            {
                _spriteBatch.Draw(TextureCache[command.TextureName].Texture2D, command.DestRect, command.SrcRect, Color.White);
            }

            _spriteBatch.End();

            _graphics.SetRenderTarget(null);

            _drawCommands.Clear();

        }

        public void DrawSprite(string spriteName, string frameName, int x, int y)
        {
            var cell = SpriteCache[spriteName].Cells[frameName];
            DrawTexture(spriteName,
                new int[] { cell.X, cell.Y, cell.Width, cell.Height },
                new int[] { x, y, cell.Width, cell.Height });
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

        public void SetClearColor(byte r, byte g, byte b)
        {
            _clearColor.R = r;
            _clearColor.G = g;
            _clearColor.B = b;
            _clearColor.A = 0;
        }

        public void MakeTransparent(string textureName, byte r, byte g, byte b)
        {
            TextureCache[textureName].MakeTransparent(r, g, b);
        }
    }
}
