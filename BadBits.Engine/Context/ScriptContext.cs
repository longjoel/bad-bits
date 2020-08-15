using BadBits.Engine.Interfaces.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Context
{
    public class ScriptContext : IScriptContext
    {
        private IGraphicsContext2d _graphicsContext2D;
        private IInputContext _inputContext;

        public Action InitAction { get; private set; }

        public Action<double> Render2dAction { get; private set; }

        public Action<double> Render3dAction { get; private set; }

        public Action<double> ProcessAction { get; private set; }

        public ScriptContext(IGraphicsContext2d graphicsContext2D, IInputContext inputContext) {
            _graphicsContext2D = graphicsContext2D;
            _inputContext = inputContext;
        }

        public void createTexture(string name, int width, int height)
        {
            _graphicsContext2D.CreateTexture(name, width, height);
        }

        public void drawSprite(string name, int x, int y, int row, int col)
        {
            _graphicsContext2D.DrawSprite(name, x, y, row, col);
        }

        public void drawTexture(string name, int[] srcRect, int[] destRect)
        {
            _graphicsContext2D.DrawTexture(name, srcRect, destRect);
        }

        public void loadSpriteSheet(string name, string path, int rows, int cols)
        {
            _graphicsContext2D.LoadSpriteSheet(name, path,rows,cols);
        }

        public void loadTexture(string name, string path)
        {
            _graphicsContext2D.LoadTexture(name, path);
        }

        public void setSpriteSheet(string name, int rows, int cols)
        {
            _graphicsContext2D.SetSpriteSheet(name,  rows, cols);
        }

        public void setPixelTransparent(string textureName, int x, int y, int r, int g, int b, int a)
        {
            _graphicsContext2D.SetPixel(textureName, x, y, (byte)r, (byte)g, (byte)b, (byte)a);
        }

        public void setPixel(string textureName, int x, int y, int r, int g, int b)
        {
            _graphicsContext2D.SetPixel(textureName, x, y, (byte)r, (byte)g, (byte)b);
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

      
    }
}
