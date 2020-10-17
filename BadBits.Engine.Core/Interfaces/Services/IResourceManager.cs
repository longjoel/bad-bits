using BadBits.Engine.Models.Host.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Interfaces.Services
{
    public interface IResourceManager
    {
        Dictionary<string, Models.Host.Texture> TextureCache { get; }
        Dictionary<string, Sprite> SpriteCache { get; }
        Dictionary<string, SoundEffect> SoundEffectCache { get; }

        void CreateTexture(string name, int width, int height);
        void LoadTexture(string name, string path);

        void LoadTextureFromResource(string name, string key);
        void SetPixel(string textureName, int x, int y, Color color);
        void UpdateTextures();

        void CreateSprite(string spriteName);
        void LoadSprite(string spriteName, string path);
        void AddSpriteFrame(string spriteName, SpriteFrame spriteFrame);

        void LoadAudio(string name, string path);
    }
}
