using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Next.Interfaces.Services
{
    public class IResourceManager
    {
        Dictionary<string, Models.Host.Texture> TextureCache { get; }
        Dictionary<string, Models.Host.Sprite.Sprite> SpriteCache { get; }
    }
}
