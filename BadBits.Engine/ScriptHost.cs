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

        public Action<double> RenderFunction { get; private set; }
        public Action<double> ProcessFunction { get; private set; }

        public void setRender(Action<double> renderFunc) {
            this.RenderFunction = renderFunc;
        }

        public void setProcess(Action<double> processFunc) {
            this.ProcessFunction = processFunc;
        }

        public void info(string infoString) {
            Console.WriteLine(infoString);
        }
    }
}
