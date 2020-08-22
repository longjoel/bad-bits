using Jint.CommonJS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BadBits.Engine
{


    public class BadBitsGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;

        private Interfaces.Context.IGraphicsContext2d _graphicsContext2d;
        private RenderTarget2D _renderTarget2d;
        private Interfaces.Context.IGraphicsContext3d _graphicsContext3d;
        private RenderTarget2D _renderTarget3d;

        private RenderTarget2D _final;

        private Interfaces.Context.IInputContext _inputContext;

        private Interfaces.Context.IScriptContext _scriptContext;

        Jint.Engine _scriptEngine;
        Jint.Native.JsValue _exports;

        // dynamic _globals;

        public BadBitsGame() : base()
        {
            _graphics = new GraphicsDeviceManager(this);

        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 960;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            _graphicsContext2d = new Context.GraphicsContext2d(_graphics.GraphicsDevice, _renderTarget2d);
            _graphicsContext3d = new Context.GraphicsContex3d(_graphics.GraphicsDevice, _graphicsContext2d, _renderTarget3d);

            _inputContext = new Context.InputContext();

            _scriptContext = new Context.ScriptContext(_graphicsContext2d, _graphicsContext3d, _inputContext);

            _scriptEngine = new Jint.Engine();
            _scriptEngine.SetValue("engine", _scriptContext);

            _exports = _scriptEngine.CommonJS().RunMain(Environment.GetCommandLineArgs()[1]);

            if (_scriptContext.InitAction != null)
            {
                _scriptContext.InitAction.Invoke();
            }

            _renderTarget2d = new RenderTarget2D(_graphics.GraphicsDevice, 320, 240);
            _renderTarget3d = new RenderTarget2D(_graphics.GraphicsDevice, 320, 240);
            _final = new RenderTarget2D(_graphics.GraphicsDevice, 320, 240);

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
            GraphicsDevice.Clear(Color.Blue);

            _graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
            _graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            _graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
            _graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;

            if (_scriptContext.Render3dAction != null)
            {
                _scriptContext.Render3dAction.Invoke(gameTime.ElapsedGameTime.TotalSeconds);
                _graphicsContext3d.Render();
            }

            if (_scriptContext.Render2dAction != null)
            {
                _scriptContext.Render2dAction.Invoke(gameTime.ElapsedGameTime.TotalSeconds);
                _graphicsContext2d.Render();

            }

            _graphics.GraphicsDevice.SetRenderTarget(_final);

            _graphics.GraphicsDevice.Clear(Color.Black);
            using (var sb = new SpriteBatch(_graphics.GraphicsDevice))
            {
                sb.Begin();
                sb.Draw(_renderTarget3d, new Rectangle(0, 0, 320, 240), null, Color.White);
                sb.Draw(_renderTarget2d, new Rectangle(0, 0, 320, 240), null, Color.White);
                sb.End();
            }

            _graphics.GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(Color.Blue);

            using (var be = new BasicEffect(_graphics.GraphicsDevice)) {

                foreach (var p in be.CurrentTechnique.Passes) {

                    be.Texture = _final;
                    be.TextureEnabled = true;
                    be.View = Matrix.CreateOrthographicOffCenter(-.005f, 1.005f, 1.005f, -.005f, -1, 1);
                    _graphics.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
                    _graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;
                    
                    p.Apply();

                    _graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, new VertexPositionTexture[] {
                        new VertexPositionTexture{Position = new Vector3(0,0,0), TextureCoordinate = new Vector2(0,0) },
                        new VertexPositionTexture{Position = new Vector3(0,1,0), TextureCoordinate = new Vector2(0,1) },
                        new VertexPositionTexture{Position = new Vector3(1,1,0), TextureCoordinate = new Vector2(1,1) },

                        new VertexPositionTexture{Position = new Vector3(1,1,0), TextureCoordinate = new Vector2(1,1) },
                        new VertexPositionTexture{Position = new Vector3(1,0,0), TextureCoordinate = new Vector2(1,0) },
                        new VertexPositionTexture{Position = new Vector3(0,0,0), TextureCoordinate = new Vector2(0,0) },

                    }, 0, 2);

                }
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

            using (var game = new BadBitsGame())
            {
                game.Run();
            }

        }
    }
}
