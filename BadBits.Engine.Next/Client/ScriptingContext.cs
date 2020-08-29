using BadBits.Engine.Next.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Next.Client
{
    public class ScriptingContext : IScriptingContext
    {
        public Action<double, IGraphicsContext2d> DrawBackgroundCallback { get; private set; }

        public Action<double, IGraphicsContext2d> DrawForegroundCallback { get; private set; }

        public Action<double, IGraphicsContext3d> Draw3dCallback { get; private set; }

        public Action<double> ProcessCallback { get; private set; }

        public Action InitCallback { get; private set; }

        public Action CloseCallback { get; private set; }

        public void setClose(Action closeCallback)
        {
            CloseCallback = closeCallback;
        }

        public void setDraw3d(Action<double, IGraphicsContext3d> renderCallback)
        {
            Draw3dCallback = Draw3dCallback;
        }

        public void setDrawBackground(Action<double, IGraphicsContext2d> renderCallback)
        {
            DrawBackgroundCallback = renderCallback;
        }

        public void setDrawForeground(Action<double, IGraphicsContext2d> renderCallback)
        {
            DrawForegroundCallback = renderCallback;
        }

        public void setInit(Action initCallback)
        {
            InitCallback = initCallback;
        }

        public void setProcess(Action<double> processCallback)
        {
            ProcessCallback = processCallback;
        }
    }
}
