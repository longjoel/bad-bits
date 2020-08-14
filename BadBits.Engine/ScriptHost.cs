using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jint;
using Microsoft.Xna.Framework.Graphics;

namespace BadBits.Engine
{
    public class ScriptHost
    {
        private GraphicsDevice _graphics;

        public Action<double,IGraphics2D> Render2dFunction { get; private set; }
        public Action<double> Render3dFunction { get; private set; }
        public Action<double> ProcessFunction { get; private set; }
        public Action InitFunction { get; private set; }

        public Dictionary<string,Texture2D> Textures { get; private set; }

        public ScriptHost(GraphicsDevice graphics) {
            _graphics = graphics;

            Textures = new Dictionary<string, Texture2D>();
        }

        public void setInit(Action initFunc) {
            this.InitFunction = initFunc;
        }

        public void setRender2d(Action<double, IGraphics2D> renderFunc) {
            this.Render2dFunction = renderFunc;
        }

        public void setRender3d(Action<double> renderFunc)
        {
            this.Render3dFunction = renderFunc;
        }

        public void setProcess(Action<double> processFunc) {
            this.ProcessFunction = processFunc;
        }

        public void info(string infoString) {
            Console.WriteLine(infoString);
        }
        
    }
}
