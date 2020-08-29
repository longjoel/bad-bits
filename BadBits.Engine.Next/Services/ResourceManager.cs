using BadBits.Engine.Next.Interfaces.Services;
using BadBits.Engine.Next.Models.Host;
using BadBits.Engine.Next.Models.Host.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texture = BadBits.Engine.Next.Models.Host.Texture;

namespace BadBits.Engine.Next.Services
{
    public class ResourceManager : IResourceManager
    {
        private GraphicsDevice _graphicsDevice;

        public Dictionary<string, Texture> TextureCache { get; private set; }

        public Dictionary<string, Sprite> SpriteCache {get; private set;}

        public ResourceManager(GraphicsDevice graphicsDevice) {
            _graphicsDevice = graphicsDevice;
            TextureCache = new Dictionary<string, Texture>();
            SpriteCache = new Dictionary<string, Sprite>();
        }


        public void AddSpriteFrame(string spriteName, SpriteFrame spriteFrame)
        {
            if (!SpriteCache.ContainsKey(spriteName)) {
                SpriteCache[spriteName] = new Sprite();
                SpriteCache[spriteName].Add(spriteFrame);
            }
        }

        public void CreateSprite(string spriteName)
        {
            if (!SpriteCache.ContainsKey(spriteName))
            {
                SpriteCache[spriteName] = new Sprite();
            }
        }

        public void CreateTexture(string name, int width, int height)
        {
            var newTexture = new Texture(_graphicsDevice, width, height);
            TextureCache[name] = newTexture;
        }

        public void LoadSprite(string spriteName, string path)
        {
            throw new NotImplementedException();
        }

        public void LoadTexture(string name, string path)
        {
            throw new NotImplementedException();
        }

        public void SetPixel(string textureName, int x, int y, Color color)
        {
            throw new NotImplementedException();
        }

        public void UpdateTextures()
        {
            throw new NotImplementedException();
        }
    }
}
