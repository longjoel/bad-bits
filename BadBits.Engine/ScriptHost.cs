using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jint;

using gl = OpenTK.Graphics.OpenGL.GL;

namespace BadBits.Engine
{
    public class ScriptHost
    {
        public Action<double> Render2dFunction { get; private set; }
        public Action<double> Render3dFunction { get; private set; }
        public Action<double> ProcessFunction { get; private set; }
        public Action InitFunction { get; private set; }

        public void setInit(Action initFunc) {
            this.InitFunction = initFunc;
        }

        public void setRender2d(Action<double> renderFunc) {
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

        public void setPixel2d(float x, float y, float r, float g, float b) {

            gl.Color3(r, g, b);

            gl.Begin(OpenTK.Graphics.OpenGL.PrimitiveType.Quads);

            gl.Color3(r, g, b);

            gl.Vertex3(x, y, 0);
            gl.Vertex3(x + 1, y, 0);
            gl.Vertex3(x + 1, y + 1, 0);
            gl.Vertex3(x, y + 1, 0);

            //gl.Vertex3(0,0, 0);
            //gl.Vertex3(1, 0, 0);
            //gl.Vertex3( 1, 1, 0);
            //gl.Vertex3(0,  1, 0);

            gl.End();
        }
    }
}
