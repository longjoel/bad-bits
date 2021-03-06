﻿using BadBits.Engine.Client;
using Jint.CommonJS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace BadBits.Engine
{
    class GameInstance : Game
    {
        Jint.Engine _engine;
        GraphicsDeviceManager _graphicsDeviceManager;

        Interfaces.Client.IGraphicsContext2d _backgroundGraphicsContext;
        Interfaces.Client.IGraphicsContext2d _foregroundGraphicsContext;
        Interfaces.Client.IGraphicsContext3d _graphicsContext3d;
        Interfaces.Client.IScriptingContext _scriptingContext;
        Interfaces.Client.IInputContext _inputContext;
        Interfaces.Client.IAudioContext _audioContext;

        Interfaces.Services.IResourceManager _resourceManager;
        RenderTarget2D _backgroundRenderTarget;
        RenderTarget2D _graphics3dTarget;
        RenderTarget2D _foregroundRenderTarget;
        BasicEffect _mainEffect;
        private BasicEffect _flatShadedEffect;
        private BasicEffect _texturedEffect;

        SpriteBatch _spriteBatch;

        public GameInstance() : base()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;
        }

        private void DrawForeground(GameTime gameTime, List<Texture2D> drawChain)
        {
            if (_scriptingContext.DrawForegroundCallback != null)
            {
                GraphicsDevice.SetRenderTarget(_foregroundRenderTarget);
                GraphicsDevice.Clear(Color.Transparent);
                _scriptingContext.DrawForegroundCallback.Invoke(gameTime.ElapsedGameTime.TotalSeconds, _foregroundGraphicsContext);

                _resourceManager.UpdateTextures();
                _spriteBatch.Begin(SpriteSortMode.Immediate,
                    BlendState.AlphaBlend,
                    SamplerState.PointClamp);

                _foregroundGraphicsContext.DrawCommands.ForEach(x => _spriteBatch.Draw(_resourceManager.TextureCache[x.TextureName], x.Dest, x.Source, Color.White));

                _spriteBatch.End();

                _foregroundGraphicsContext.DrawCommands.Clear();
                drawChain.Add(_foregroundRenderTarget);
            }
        }

        private void DrawBackground(GameTime gameTime, List<Texture2D> drawChain)
        {
            if (_scriptingContext.DrawBackgroundCallback != null)
            {
                GraphicsDevice.SetRenderTarget(_backgroundRenderTarget);
                GraphicsDevice.Clear(Color.Transparent);
                _scriptingContext.DrawBackgroundCallback.Invoke(gameTime.ElapsedGameTime.TotalSeconds, _backgroundGraphicsContext);

                _resourceManager.UpdateTextures();
                _spriteBatch.Begin(SpriteSortMode.Immediate,
                    BlendState.AlphaBlend,
                    SamplerState.PointClamp);

                _backgroundGraphicsContext.DrawCommands.ForEach(x => _spriteBatch.Draw(_resourceManager.TextureCache[x.TextureName], x.Dest, x.Source, Color.White));

                _spriteBatch.End();

                _backgroundGraphicsContext.DrawCommands.Clear();
                drawChain.Add(_backgroundRenderTarget);

            }
        }

        private void Draw3d(GameTime gameTime, List<Texture2D> drawChain)
        {
            if (_scriptingContext.Draw3dCallback != null)
            {

                GraphicsDevice.SetRenderTarget(_graphics3dTarget);
                GraphicsDevice.Clear(Color.Transparent);

                _scriptingContext.Draw3dCallback.Invoke(gameTime.ElapsedGameTime.TotalSeconds, _graphicsContext3d);


                _resourceManager.UpdateTextures();

                foreach (var p in _flatShadedEffect.CurrentTechnique.Passes)
                {
                    _flatShadedEffect.Projection = _graphicsContext3d.ProjectionMatrix;
                   _flatShadedEffect.View = _graphicsContext3d.ViewMatrix;

                    p.Apply();

                    GraphicsDevice.RasterizerState = RasterizerState.CullNone;
                    GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                    GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                    foreach (var m in _graphicsContext3d.MeshesByColor) {

                        _flatShadedEffect.World = m.Item2;
                        GraphicsDevice.SetVertexBuffer(m.Item1.Buffer);
                        GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, m.Item1.NumTriangles * 3);
                    }

                    _flatShadedEffect.World = Matrix.Identity;

                    foreach (var kvp in _graphicsContext3d.TrianglesByColor)
                    {
                        var color = kvp.Key;

                        var vx = kvp.Value.Select(v => new VertexPositionColor(v.Position, color)).ToArray();

                        GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList,
                           vx.ToArray(), 0, kvp.Value.Count / 3);
                    }

                    

                }


                foreach (var p in _texturedEffect.CurrentTechnique.Passes)
                {
                    _texturedEffect.Projection = _graphicsContext3d.ProjectionMatrix;
                    _texturedEffect.View = _graphicsContext3d.ViewMatrix;
                    GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;


                    p.Apply();

                    GraphicsDevice.RasterizerState = RasterizerState.CullNone;
                    GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                    GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                    foreach (var kvp in _graphicsContext3d.MeshesByTexture)
                    {
                        var texture = kvp.Key;

                        foreach (var m in kvp.Value) {
                            _texturedEffect.Texture = texture;
                            _texturedEffect.TextureEnabled = true;

                            _texturedEffect.World = m.Item2;
                            GraphicsDevice.SetVertexBuffer(m.Item1.Buffer);
                            GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, m.Item1.NumTriangles * 3);
                        }

                    }

                    _flatShadedEffect.World = Matrix.Identity;

                    GraphicsDevice.RasterizerState = RasterizerState.CullNone;
                    GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                    GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                    foreach (var kvp in _graphicsContext3d.TrianglesByTexture)
                    {
                        var texture = kvp.Key;
                        _texturedEffect.Texture = texture;
                        _texturedEffect.TextureEnabled = true;

                        GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList,
                            kvp.Value.Select(v => new VertexPositionTexture(v.Position, v.TextureCoordinate)).ToArray(), 0, kvp.Value.Count / 3);
                    }
                }

                _graphicsContext3d.TrianglesByColor.Clear();
                _graphicsContext3d.TrianglesByTexture.Clear();
                _graphicsContext3d.MeshesByColor.Clear();
                _graphicsContext3d.MeshesByTexture.Clear();

                drawChain.Add(_graphics3dTarget);
            }
        }


        protected override void Initialize()
        {


            _graphicsDeviceManager.PreferredBackBufferWidth = 960;
            _graphicsDeviceManager.PreferredBackBufferHeight = 720;
            _graphicsDeviceManager.ApplyChanges();

            _engine = new Jint.Engine(cfg => cfg.Strict(false).AllowClr(typeof(GameInstance).Assembly));
            _resourceManager = new Services.ResourceManager(GraphicsDevice);

            _audioContext = new AudioContext(_resourceManager);


            _backgroundRenderTarget = new RenderTarget2D(GraphicsDevice, 320, 240);
            _graphics3dTarget = new RenderTarget2D(GraphicsDevice, 320, 240, false, SurfaceFormat.Color, DepthFormat.Depth24);
            _foregroundRenderTarget = new RenderTarget2D(GraphicsDevice, 320, 240);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _mainEffect = new BasicEffect(GraphicsDevice);

            _flatShadedEffect = new BasicEffect(GraphicsDevice)
            {
                FogEnabled = true,
                FogColor = new Vector3(),
                FogStart = 90,
                FogEnd = 100,
                LightingEnabled = false,
                TextureEnabled = false,
                VertexColorEnabled = true
            };

            _texturedEffect = new BasicEffect(GraphicsDevice)
            {
                FogEnabled = true,
                FogColor = new Vector3(),
                FogStart = 90,
                FogEnd = 100,
                LightingEnabled = false,
                TextureEnabled = true
            };

            _scriptingContext = new ScriptingContext(_resourceManager);
            _graphicsContext3d = new GraphicsContext3d(_resourceManager);
            _backgroundGraphicsContext = new GraphicsContext2d(_resourceManager);
            _foregroundGraphicsContext = new GraphicsContext2d(_resourceManager);

            _inputContext = new InputContext();


            _engine.SetValue("engine", _scriptingContext);


            string pathToIndex = "";
            string cwd = Environment.CurrentDirectory;

            // was a path explicitly stated?
            if (Environment.GetCommandLineArgs().Length > 1)
            {
                pathToIndex = Environment.GetCommandLineArgs()[1];
            }
            // is index in root?
            else if (File.Exists("index.js")) {
                pathToIndex = "index.js";
            }
            else {
                // Where is it?
                var dirs = Directory.EnumerateDirectories("..");
                pathToIndex = dirs.FirstOrDefault(x => File.Exists(Path.Combine(x, "index.js")));

            }

            if (string.IsNullOrEmpty(pathToIndex)) {
                // can we still not find anything?
                Console.WriteLine("Can not find index.js... Exiting");
                return;
            }

            _engine.CommonJS().RunMain(pathToIndex);


            _resourceManager.CreateTexture("__dither", 320, 240);
            var r = new Random(4206932);
            for (int y = 0; y < 240; y++)
            {
                for (int x = 0; x < 320; x++)
                {
                    var c = Color.Transparent;

                    if ((x+y)%2==0) {
                        c = new Color(0, 0, 0, (byte)r.Next()%16);
                    }
                    _resourceManager.SetPixel("__dither",x, y, c);
                }
            }

            _resourceManager.UpdateTextures();

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
            var drawChain = new List<Texture2D>();

            if ((_scriptingContext.AssetsToLoad == null) || (_scriptingContext.AssetsToLoad.Count == 0))
            {

                DrawBackground(gameTime, drawChain);

                Draw3d(gameTime, drawChain);

                DrawForeground(gameTime, drawChain);
            }
            { 
            
            }

            drawChain.Add(_resourceManager.TextureCache["__dither"]);
            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(Color.Black);

            float z = 0.25f;

            foreach (var pass in _mainEffect.CurrentTechnique.Passes)
            {

                foreach (var t in drawChain)
                {
                    _mainEffect.Texture = t;
                    _mainEffect.TextureEnabled = true;
                    _mainEffect.VertexColorEnabled = false;

                    _mainEffect.View = Matrix.CreateOrthographicOffCenter(0, 1, 1, 0, -1, 1);
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

                    z -= 0.05f;

                }

            }

            base.Draw(gameTime);
        }




        protected override void Update(GameTime gameTime)
        {

            _scriptingContext.ProcessCallback?.Invoke(gameTime.ElapsedGameTime.TotalSeconds, _inputContext, _audioContext);

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
            using (var game = new GameInstance())
            {

                game.Run();
            }
        }
    }
}
