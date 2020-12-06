﻿using BadBits.Engine.Interfaces.Client;
using BadBits.Engine.Interfaces.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BadBits.Engine.Client
{
    public class ScriptingContext : IScriptingContext
    {
        private IResourceManager _resourceManager;

        public Action<double, IGraphicsContext2d> DrawBackgroundCallback { get; private set; }

        public Action<double, IGraphicsContext2d> DrawForegroundCallback { get; private set; }

        public Action<double, IGraphicsContext3d> Draw3dCallback { get; private set; }

        public Action<double, IInputContext, IAudioContext> ProcessCallback { get; private set; }

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

        public void setProcess(Action<double, IInputContext, IAudioContext> processCallback)
        {
            ProcessCallback = processCallback;
        }

        public object getTextureAttributes(string name)
        {
            var t = _resourceManager.TextureCache[name];
            return new { x = 0, y = 0, width = t.Width, height = t.Height };
        }

        public void loadSprite(string name, string path)
        {

            _resourceManager.LoadSprite(name, path);
        }

        public void log(string value)
        {
            Console.WriteLine(value);
        }

        public void loadAudio(string name, string path)
        {
            _resourceManager.LoadAudio(name, path);
        }

        public void createTexturedMesh(string meshName, string textureName, object[] meshData)
        {

            var mesh = new List<VertexPositionTexture>();

            foreach (var v in meshData)
            {

                dynamic vv = v;

                mesh.Add(new VertexPositionTexture
                {
                    Position = new Vector3((float)vv.x, (float)vv.y, (float)vv.z),
                    TextureCoordinate = new Vector2((float)vv.u, (float)vv.v)
                });
            }

            _resourceManager.CreateMesh(meshName, textureName, mesh);
        }

        public void createColoredMesh(string meshName, object color, object[] meshData)
        {
            var mesh = new List<VertexPosition>();
            dynamic cc = color;

            foreach (var v in meshData)
            {

                dynamic vv = v;
                mesh.Add(new VertexPosition
                {
                    Position = new Vector3((float)vv.x, (float)vv.y, (float)vv.z)
                });
            }

            _resourceManager.CreateMesh(meshName, new Color((byte)cc.r, (byte)cc.g, (byte)cc.b), mesh);
        }
    }
}
