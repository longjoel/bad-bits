using CommandLine;
using Jint.CommonJS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BadBits.Engine
{
    /// <summary>
    /// OK - Notes
    /// 
    /// Step 1 - make sure the file is real and it's there. If it's not there, then bail.
    /// 
    /// Step 2 - initialize the engine. 
    ///     Create the window with a loading screen
    ///     setup the open gl settings
    ///     setup the bindings
    ///     
    /// Step 3 - invoke the entrypoint script.
    /// 
    /// the script should call register() with an object that looks like this
    ///     { onRender : (dt, ctx)=> void,
    ///         onFrame: (dt) => void,
    ///         onClose: ()=> void,
    ///         onInit: ()=> void}
    ///         


    class Game : Microsoft.Xna.Framework.Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Graphics2D _graphics2D;
        Engine.ScriptHost _host;
        Jint.Engine _scriptEngine; 
        Jint.Native.JsValue _exports;

        dynamic _globals;

        Game() : base() {
            this._graphics = new GraphicsDeviceManager(this);

            _host = new ScriptHost();
            _scriptEngine = new Jint.Engine();
            _globals = new { };
            _scriptEngine.SetValue("globals", _globals);
            _scriptEngine.SetValue("badBits", _host);

            _exports = _scriptEngine.CommonJS().RunMain(Environment.GetCommandLineArgs()[0]);

        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(this.GraphicsDevice);
            _graphics2D = new Graphics2D();
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

    static class Program
    {

     
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

          


            //using (var window = new Microsoft.Xna.Framework.Game())
            //{
            //    var adapter = new Microsoft.Xna.Framework.Graphics.GraphicsAdapter();
                
                

            //    window.Load += (o, e) =>
            //    {
            //        if (!window.Context.IsCurrent)
            //        {
            //            window.Context.MakeCurrent(window.WindowInfo);
            //        }

            //        gfx2d = new Graphics2D();

            //    };


            //    window.Bounds = new Rectangle { X = 0, Y = 0, Width = 640, Height = 480 };
            //    window.RenderFrame += (o, e) =>
            //    {
            //        gl.Viewport(new Rectangle(0, 0, window.Width, window.Height));
            //        gl.ClearColor(Color.Black);
            //        gl.Clear(OpenTK.Graphics.OpenGL.ClearBufferMask.ColorBufferBit
            //            | OpenTK.Graphics.OpenGL.ClearBufferMask.DepthBufferBit);


            //        if (host.Render3dFunction != null)
            //        {
            //            host.Render3dFunction.Invoke(e.Time);
            //        }

            //        if (host.Render2dFunction != null)
            //        {


            //            host.Render2dFunction.Invoke(e.Time, gfx2d);
            //        }

            //        window.SwapBuffers();
            //    };

            //    window.UpdateFrame += (o, e) =>
            //    {
            //        if (host.ProcessFunction != null)
            //        {
            //            host.ProcessFunction.Invoke(e.Time);
            //        }
            //    };
            //    window.Run();

            //}

        }
    }
}
