using Jint.CommonJS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BadBits.Engine
{
 

    public class BadBitsGame : Microsoft.Xna.Framework.Game {
        private GraphicsDeviceManager _graphics;

        private Interfaces.Context.IGraphicsContext2d _graphicsContext2d;
        private Interfaces.Context.IGraphicsContext3d _graphicsContext3d;
        private Interfaces.Context.IInputContext _inputContext;

        private Interfaces.Context.IScriptContext _scriptContext;

        Jint.Engine _scriptEngine; 
        Jint.Native.JsValue _exports;

       // dynamic _globals;

        public BadBitsGame() : base() {
            _graphics = new GraphicsDeviceManager(this);

        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 960;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            _graphicsContext2d = new Context.GraphicsContext2d(_graphics.GraphicsDevice);
            _graphicsContext3d = new Context.GraphicsContex3d(_graphics.GraphicsDevice, _graphicsContext2d);

            _inputContext = new Context.InputContext();

            _scriptContext = new Context.ScriptContext(_graphicsContext2d,_graphicsContext3d, _inputContext);

            _scriptEngine = new Jint.Engine();
            _scriptEngine.SetValue("engine", _scriptContext);

            _exports = _scriptEngine.CommonJS().RunMain(Environment.GetCommandLineArgs()[1]);

            if (_scriptContext.InitAction != null) {
                _scriptContext.InitAction.Invoke();
            }

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (_scriptContext.ProcessAction != null)
            {
                _scriptContext.ProcessAction.Invoke(gameTime.ElapsedGameTime.TotalSeconds);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            if (_scriptContext.Render3dAction != null) {
                _scriptContext.Render3dAction.Invoke(gameTime.ElapsedGameTime.TotalSeconds);
                _graphicsContext3d.Render();
            }

            if (_scriptContext.Render2dAction != null)
            {
                _scriptContext.Render2dAction.Invoke(gameTime.ElapsedGameTime.TotalSeconds);
                _graphicsContext2d.Render();

            }
            base.Draw(gameTime);
        } 
    }

    public static class Program
    {

     
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            using (var game = new BadBitsGame()) {
                game.Run();
            }

        }
    }
}
