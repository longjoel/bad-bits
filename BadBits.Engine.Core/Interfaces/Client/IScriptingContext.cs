using BadBits.Engine.Models.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Interfaces.Client
{
    
    public interface IScriptingContext
    {
        Action<double, IGraphicsContext2d> DrawBackgroundCallback { get; }
        Action<double, IGraphicsContext2d> DrawForegroundCallback { get; }
        Action<double, IGraphicsContext3d> Draw3dCallback { get; }
        Action<double, IInputContext, IAudioContext> ProcessCallback { get; }
        Action InitCallback { get; }
        Action CloseCallback { get; }

        Action<double,IGraphicsContext2d> LoadingFunc { get; set; }
        Action LoadComplete { get; set; }

        List<Asset> AssetsToLoad { get; set; }


        void setDrawBackground(Action<double, IGraphicsContext2d> renderCallback);
        void setDrawForeground(Action<double, IGraphicsContext2d> renderCallback);
        void setDraw3d(Action<double, IGraphicsContext3d> renderCallback);

        void setProcess(Action<double,IInputContext, IAudioContext> processCallback);
        void setInit(Action initCallback);
        void setClose(Action closeCallback);

        void createTexturedMesh(string meshName, string textureName, object[] meshData);

        void createColoredMesh(string meshName, object color, object[] meshData);

        void loadTexture(string name, string path);
        void createTexture(string name, int width, int height);
        void setPixel(string name, int x, int y, object color);
        void makeTransparent(string name, object color);

        void loadSprite(string name, string path);

        void loadAudio(string name, string path);

        object getTextureAttributes(string name);

        void log(string value);

        void clearCache();

        void loadScreen(object[] itemsToLoad, Action<double, IGraphicsContext2d> loadingScreenFunc, Action onLoadingComplete);

    }
}
