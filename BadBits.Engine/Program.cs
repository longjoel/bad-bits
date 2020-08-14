using CommandLine;
using Jint.CommonJS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BadBits.Engine
{
 

    public class BadBitsGame : Microsoft.Xna.Framework.Game {
        private GraphicsDeviceManager _graphics;
        private IGraphics2D _graphics2D;
        Engine.ScriptHost _host;
        Jint.Engine _scriptEngine; 
        Jint.Native.JsValue _exports;

        dynamic _globals;

        public BadBitsGame() : base() {
            this._graphics = new GraphicsDeviceManager(this);

            _host = new ScriptHost(GraphicsDevice);
            _scriptEngine = new Jint.Engine();
            _globals = new { };
            _scriptEngine.SetValue("globals", _globals);
            _scriptEngine.SetValue("badBits", _host);

            _exports = _scriptEngine.CommonJS().RunMain(Environment.GetCommandLineArgs()[1]);

        }

        protected override void Initialize()
        {
            _graphics2D = new Graphics2d(this.GraphicsDevice);
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            if (_host.ProcessFunction != null)
            {
                _host.ProcessFunction.Invoke(gameTime.ElapsedGameTime.TotalSeconds);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            if (_host.Render3dFunction != null)
            {
                _host.Render3dFunction.Invoke(gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (_host.Render2dFunction != null)
            {
                _host.Render2dFunction.Invoke(gameTime.ElapsedGameTime.TotalSeconds, _graphics2D);
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
