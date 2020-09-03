using BadBits.Engine.Next.Interfaces.Client;
using BadBits.Engine.Next.Interfaces.Services;
using Microsoft.Xna.Framework;
using System;

namespace BadBits.Engine.Next.Client
{
    public class ScriptingContext : IScriptingContext
    {
        private IResourceManager _resourceManager;

        public Action<double, IGraphicsContext2d> DrawBackgroundCallback { get; private set; }

        public Action<double, IGraphicsContext2d> DrawForegroundCallback { get; private set; }

        public Action<double, IGraphicsContext3d> Draw3dCallback { get; private set; }

        public Action<double, IInputContext> ProcessCallback { get; private set; }

        public Action InitCallback { get; private set; }

        public Action CloseCallback { get; private set; }

        public ScriptingContext(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public void createTexture(string name, int width, int height)
        {
            _resourceManager.CreateTexture(name, width, height);

        }

        public void loadTexture(string name, string path)
        {
            _resourceManager.LoadTexture(name, path);
        }

        public void makeTransparent(string name, object color)
        {
            dynamic c = color;

            _resourceManager.TextureCache[name].MakeTransparent(new Color((byte)c.r, (byte)c.g, (byte)c.b));
        }

        public void setClose(Action closeCallback)
        {
            CloseCallback = closeCallback;
        }

        public void setDraw3d(Action<double, IGraphicsContext3d> renderCallback)
        {
            Draw3dCallback = renderCallback;
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

        public void setPixel(string name, int x, int y, object color)
        {
            dynamic c = color;
            _resourceManager.TextureCache[name].SetPixel(x, y, new Color((byte)c.r, (byte)c.g, (byte)c.b));
        }

        public void setProcess(Action<double, IInputContext> processCallback)
        {
            ProcessCallback = processCallback;
        }

        public object getTextureAttributes(string name)
        {
            var t = _resourceManager.TextureCache[name];
            return new { x = 0, y = 0, width = t.Width, height = t.Height };
        }

        public void log(string value)
        {
            Console.WriteLine(value);
        }
    }
}
