using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Interfaces.Shared
{
    public interface ISharedAssets
    {

        Dictionary<string, Model.Texture> TextureCache { get; }
        Dictionary<string, Model.SpriteSheet> SpriteCache { get; }

        void CreateTexture(string name, int width, int height);
        void LoadTexture(string name, string path);
        void LoadSpriteSheet(string name, string path, string spriteSheetPath);

    }
}
