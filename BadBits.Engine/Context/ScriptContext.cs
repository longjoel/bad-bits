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
       

        public ScriptContext(IGraphicsContext2d graphicsContext2D) {
            _graphicsContext2D = graphicsContext2D;
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

        public void setInit(Action initAction)
        {
            throw new NotImplementedException();
        }

        public void setPixel(string textureName, int x, int y, byte r, byte g, byte b, byte a)
        {
            _graphicsContext2D.SetPixel(textureName, x, y, r, g, b, a);
        }

        public void setPixel(string textureName, int x, int y, byte r, byte g, byte b)
        {
            _graphicsContext2D.SetPixel(textureName, x, y, r, g, b);
        }

        public void setProcess(Action<double> processAction)
        {
            throw new NotImplementedException();
        }

        public void setRender2d(Action<double, IGraphicsContext2d> render2dAction)
        {
            throw new NotImplementedException();
        }

        public void setRender3d(Action<double, IGraphicsContext3d> render3dAction)
        {
            throw new NotImplementedException();
        }

        public void setSpriteSheet(string name, string textureName, int rows, int cols)
        {
            throw new NotImplementedException();
        }
    }
}
