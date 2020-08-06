using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Engine
{
    public class ScriptHost
    {
        public Action<double> RenderFunction { get; private set; }
        public Action<double> ProcessFunction { get; private set; }

        public ScriptHost() { }

        public void SetRender(Action<double> renderFunc) {
            this.RenderFunction = renderFunc;
        }

        public void SetProcess(Action<double> processFunc) {
            this.ProcessFunction = processFunc;
        }

        public void Info(string infoString) {
            Console.WriteLine(infoString);
        }
    }
}
