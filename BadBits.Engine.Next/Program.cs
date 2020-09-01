using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadBits.Engine.Next.Client;
using Jint.CommonJS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BadBits.Engine.Next
{
    class GameInstance : Game {

        Jint.Engine _engine;
        GraphicsDeviceManager _graphicsDeviceManager;

        Interfaces.Client.IGraphicsContext2d _backgroundGraphicsContext;
        Interfaces.Client.IGraphicsContext2d _foregroundGraphicsContext;
        Interfaces.Client.IGraphicsContext3d _graphicsContext3d;
        Interfaces.Client.IScriptingContext _scriptingContext;
        Interfaces.Client.IInputContext _inputContext;

        Interfaces.Services.IResourceManager _resourceManager;

        RenderTarget2D _backgroundRenderTarget;
        RenderTarget2D _graphics3dTarget;
        RenderTarget2D _foregroundRenderTarget;

        BasicEffect _mainEffect;

        SpriteBatch _spriteBatch;

        private void DrawForeground(GameTime gameTime, List<RenderTarget2D> drawChain)
        {
            if (_scriptingContext.DrawForegroundCallback != null)
            {
                GraphicsDevice.SetRenderTarget(_foregroundRenderTarget);
                GraphicsDevice.Clear(Color.Transparent);
                _scriptingContext.DrawBackgroundCallback.Invoke(gameTime.ElapsedGameTime.TotalSeconds, _foregroundGraphicsContext);

                _resourceManager.UpdateTextures();

                _spriteBatch.Begin();

                _foregroundGraphicsContext.DrawCommands.ForEach(x => _spriteBatch.Draw(_resourceManager.TextureCache[x.TextureName], x.Dest, x.Source, Color.White));

                _spriteBatch.End();

                _backgroundGraphicsContext.DrawCommands.Clear();
                drawChain.Add(_foregroundRenderTarget);
            }
        }

        private void DrawBackground(GameTime gameTime, List<RenderTarget2D> drawChain)
        {
            if (_scriptingContext.DrawBackgroundCallback != null)
            {
                GraphicsDevice.SetRenderTarget(_backgroundRenderTarget);
                GraphicsDevice.Clear(Color.Transparent);
                _scriptingContext.DrawBackgroundCallback.Invoke(gameTime.ElapsedGameTime.TotalSeconds, _backgroundGraphicsContext);

                _resourceManager.UpdateTextures();

                _spriteBatch.Begin();

                _backgroundGraphicsContext.DrawCommands.ForEach(x => _spriteBatch.Draw(_resourceManager.TextureCache[x.TextureName], x.Dest, x.Source, Color.White));

                _spriteBatch.End();

                _backgroundGraphicsContext.DrawCommands.Clear();
                drawChain.Add(_backgroundRenderTarget);

            }
        }


        protected override void Initialize()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);

            _graphicsDeviceManager.PreferredBackBufferWidth = 960;
            _graphicsDeviceManager.PreferredBackBufferHeight = 720;
            _graphicsDeviceManager.ApplyChanges();

            _engine = new Jint.Engine();
            _resourceManager = new Services.ResourceManager(GraphicsDevice);


            _backgroundRenderTarget = new RenderTarget2D(GraphicsDevice, 320, 240);
            _graphics3dTarget = new RenderTarget2D(GraphicsDevice, 320, 240);
            _foregroundRenderTarget = new RenderTarget2D(GraphicsDevice, 320, 240);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _mainEffect = new BasicEffect(GraphicsDevice);

            _scriptingContext = new ScriptingContext(_resourceManager);
            _graphicsContext3d = new GraphicsContext3d();
            _backgroundGraphicsContext = new GraphicsContext2d(_resourceManager);
            _foregroundGraphicsContext = new GraphicsContext2d(_resourceManager);



            _engine.SetValue("engine", _scriptingContext);

            _engine.CommonJS().RunMain(Environment.GetCommandLineArgs()[1]);

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
            var drawChain = new List<RenderTarget2D>();

            DrawBackground(gameTime, drawChain);

            if (_scriptingContext.Draw3dCallback != null)
            {
                GraphicsDevice.SetRenderTarget(_graphics3dTarget);
                GraphicsDevice.Clear(Color.Transparent);
                _scriptingContext.Draw3dCallback.Invoke(gameTime.ElapsedGameTime.TotalSeconds, _graphicsContext3d);

                drawChain.Add(_graphics3dTarget);
            }

            DrawForeground(gameTime, drawChain);

            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(Color.Black);

            float z = -.25f;

            foreach (var pass in _mainEffect.CurrentTechnique.Passes) {

                foreach (var t in drawChain)
                {
                    _mainEffect.Texture = t;
                    
                    pass.Apply();

                    GraphicsDevice.BlendState = BlendState.AlphaBlend;
                    GraphicsDevice.RasterizerState = RasterizerState.CullNone;
                    GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                    
                    GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, new VertexPositionTexture[] {
                        new VertexPositionTexture(new Vector3(0,0,z),new Vector2(0,0)),
                        new VertexPositionTexture(new Vector3(1,0,z),new Vector2(1,0)),
                        new VertexPositionTexture(new Vector3(1,1,z),new Vector2(1,1)),

                        new VertexPositionTexture(new Vector3(1,1,z),new Vector2(1,1)),
                        new VertexPositionTexture(new Vector3(0,1,z),new Vector2(0,1)),
                        new VertexPositionTexture(new Vector3(0,0,z),new Vector2(0,0)),

                    }, 0, 2);

                    z += 0.05f;

                }

            }

            base.Draw(gameTime);
        }

    

        protected override void Update(GameTime gameTime)
        {

            _scriptingContext.ProcessCallback?.Invoke(gameTime.ElapsedGameTime.TotalSeconds, _inputContext);

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
