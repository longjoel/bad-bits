using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jint;

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
    }
}
