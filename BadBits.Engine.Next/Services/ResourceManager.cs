using BadBits.Engine.Next.Interfaces.Services;
using BadBits.Engine.Next.Models.Host;
using BadBits.Engine.Next.Models.Host.Sprite;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Next.Services
{
    public class ResourceManager : IResourceManager
    {
        public Dictionary<string, Texture> TextureCache { get; private set; }

        public Dictionary<string, Sprite> SpriteCache {get; private set;}

        public void AddSpriteFrame(string spriteName, SpriteFrame spriteFrame)
        {
            throw new NotImplementedException();
        }

        public void CreateSprite(string spriteName)
        {
            throw new NotImplementedException();
        }

        public void CreateTexture(string name, int width, int height)
        {
            throw new NotImplementedException();
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
