using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Next.Interfaces.Client
{
    
    public interface IScriptingContext
    {
        Action<double, IGraphicsContext2d> DrawBackgroundCallback { get; }
        Action<double, IGraphicsContext2d> DrawForegroundCallback { get; }
        Action<double, IGraphicsContext3d> Draw3dCallback { get; }
        Action<double, IInputContext> ProcessCallback { get; }
        Action InitCallback { get; }
        Action CloseCallback { get; }

        void setDrawBackground(Action<double, IGraphicsContext2d> renderCallback);
        void setDrawForeground(Action<double, IGraphicsContext2d> renderCallback);
        void setDraw3d(Action<double, IGraphicsContext3d> renderCallback);

        void setProcess(Action<double,IInputContext> processCallback);
        void setInit(Action initCallback);
        void setClose(Action closeCallback);

        void loadTexture(string name, string path);
        void createTexture(string name, int width, int height);
        void setPixel(string name, int x, int y, Models.Client.Rgba color);
        void makeTransparent(string name, Models.Client.Rgb color);

        Models.Client.Rect getTextureAttributes(string name);

        void log(string value);
    }
}
