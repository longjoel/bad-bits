using BadBits.Engine.Interfaces.Context;
using BadBits.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Context
{
    public class ScriptContext : IScriptContext
    {
        private IGraphicsContext2d _graphicsContext2d;
        private IGraphicsContext3d _graphicsContext3d;
        private IInputContext _inputContext;

        public Action InitAction { get; private set; }

        public Action<double> Render2dAction { get; private set; }

        public Action<double> Render3dAction { get; private set; }

        public Action<double> ProcessAction { get; private set; }

        public ScriptContext(IGraphicsContext2d graphicsContext2d, IGraphicsContext3d graphicsContext3d, IInputContext inputContext) {
            _graphicsContext2d = graphicsContext2d;
            _graphicsContext3d = graphicsContext3d;
            _inputContext = inputContext;
        }

        public void createTexture(string name, int width, int height)
        {
            _graphicsContext2d.CreateTexture(name, width, height);
        }

        public void drawSprite(string spriteName, string frameName, int x, int y)
        {
            _graphicsContext2d.DrawSprite(spriteName,frameName,x,y);
        }

        public void drawTexture(string name, int[] srcRect, int[] destRect)
        {
            _graphicsContext2d.DrawTexture(name, srcRect, destRect);
        }

        public void loadSpriteSheet(string name, string path, string spriteSheetPath)
        {
            _graphicsContext2d.LoadSpriteSheet(name, path,spriteSheetPath);
        }

        public void loadTexture(string name, string path)
        {
            _graphicsContext2d.LoadTexture(name, path);
        }

        public void setPixelTransparent(string textureName, int x, int y, int r, int g, int b, int a)
        {
            _graphicsContext2d.SetPixel(textureName, x, y, (byte)r, (byte)g, (byte)b, (byte)a);
        }

        public void setPixel(string textureName, int x, int y, int r, int g, int b)
        {
            _graphicsContext2d.SetPixel(textureName, x, y, (byte)r, (byte)g, (byte)b);
        }

        public void setInit(Action initAction)
        {
            InitAction = initAction;
        }
        public void setProcess(Action<double> processAction)
        {
            ProcessAction = processAction;
        }

        public void setRender2d(Action<double> render2dAction)
        {
            Render2dAction = render2dAction;
        }

        public void setRender3d(Action<double> render3dAction)
        {
            Render3dAction = render3dAction;
        }

        public Model.InputState pollInput()
        {
            return _inputContext.PollInput();
        }

        public void makeTransparent(string name, int r, int g, int b)
        {
            _graphicsContext2d.MakeTransparent(name, (byte)r, (byte)g, (byte)b);
        }

        public SpriteAttribs getSpriteAttribs(string name)
        {
            var sprite = _graphicsContext2d.SpriteCache[name];

            var spriteAttribs = new SpriteAttribs();
            foreach (var s in sprite.Cells) {
                spriteAttribs[s.Key] = new SpriteCell { x = s.Value.X, y = s.Value.Y, width = s.Value.Width, height = s.Value.Height };
            }

            return spriteAttribs;
        }

        public TextureAttribs getTextureAttribs(string name)
        {
            var texture = _graphicsContext2d.TextureCache[name];

            return new TextureAttribs {
                width = texture.Width,
                height = texture.Height
            };
        }

        public void logInfo(string text)
        {
            Console.WriteLine(text);
        }

        public void drawFlatShadedTriangles(int r, int g, int b, vertexPosition[] vertices)
        {
            _graphicsContext3d.DrawFlatShadedTriangle(new Microsoft.Xna.Framework.Color((byte)r, (byte)g, (byte)b, (byte)255), 
                vertices.Select(v=> new Microsoft.Xna.Framework.Graphics.VertexPosition (new Microsoft.Xna.Framework.Vector3((float)v.x,
                (float)v.y, 
                (float)v.z))).ToList());
        }
    }
}
