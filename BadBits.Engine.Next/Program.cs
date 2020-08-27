using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BadBits.Engine.Next
{
    class GameInstance : Microsoft.Xna.Framework.Game {

        Jint.Engine _engine;
        GraphicsDeviceManager _graphicsDeviceManager;

        Interfaces.Client.IGraphicsContext2d _backgroundGraphicsContext;
        Interfaces.Client.IGraphicsContext2d _foregroundGraphicsContext;
        Interfaces.Client.IGraphicsContext3d _graphics3dContext;
        Interfaces.Client.IScriptingContext _scriptingContext;

        Interfaces.Services.IResourceManager _resourceManager;

        RenderTarget2D _backgroundRenderTarget { get; set; }
        RenderTarget2D _graphics3dTarget { get; set; }
        RenderTarget2D _foregroundRenderTarget { get; set; }

        protected override void Initialize()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            _engine = new Jint.Engine();
            _resourceManager = new Services.ResourceManager();

            _backgroundRenderTarget = new RenderTarget2D(GraphicsDevice, 320, 240);
            _graphics3dTarget = new RenderTarget2D(GraphicsDevice, 320, 240);
            _foregroundRenderTarget = new RenderTarget2D(GraphicsDevice, 320, 240);

        
            base.Initialize();
        }

        protected override void BeginRun()
        {
            if (_scriptingContext.InitCallback != null)
            {
                _scriptingContext.InitCallback.Invoke();
            }

            base.BeginRun();
        }

        protected override void Draw(GameTime gameTime)
        {
            if (_scriptingContext.DrawBackgroundCallback != null) {
                GraphicsDevice.SetRenderTarget(_backgroundRenderTarget);
                GraphicsDevice.Clear(Color.Transparent);
                _scriptingContext.DrawBackgroundCallback.Invoke(gameTime.ElapsedGameTime.TotalSeconds, _backgroundGraphicsContext);
            }

            if (_scriptingContext.Draw3dCallback != null) {
                GraphicsDevice.SetRenderTarget(_graphics3dTarget);
                GraphicsDevice.Clear(Color.Transparent);
                _scriptingContext.Draw3dCallback.Invoke(gameTime.ElapsedGameTime.TotalSeconds, _graphics3dContext);
            }

            if (_scriptingContext.DrawForegroundCallback != null)
            {
                GraphicsDevice.SetRenderTarget(_foregroundRenderTarget);
                GraphicsDevice.Clear(Color.Transparent);
                _scriptingContext.DrawBackgroundCallback.Invoke(gameTime.ElapsedGameTime.TotalSeconds, _foregroundGraphicsContext);
            }

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {

            _scriptingContext.ProcessCallback?.Invoke(gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void EndRun()
        {
            _scriptingContext.CloseCallback?.Invoke();

            base.EndRun();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new GameInstance()) {

                game.Run();
            }
        }
    }
}
