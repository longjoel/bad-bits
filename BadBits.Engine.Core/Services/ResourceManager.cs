using BadBits.Engine.Interfaces.Services;
using BadBits.Engine.Models.Host;
using BadBits.Engine.Models.Host.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texture = BadBits.Engine.Models.Host.Texture;
using Newtonsoft;
using Microsoft.Xna.Framework.Audio;

namespace BadBits.Engine.Services
{
    public class ResourceManager : IResourceManager
    {
        private GraphicsDevice _graphicsDevice;

        public Dictionary<string, Texture> TextureCache { get; private set; }

        public Dictionary<string, Sprite> SpriteCache {get; private set;}

        public Dictionary<string, SoundEffect> SoundEffectCache { get; private set; }

        public Dictionary<string, Mesh> MeshCache { get; private set; }

        public ResourceManager(GraphicsDevice graphicsDevice) {
            _graphicsDevice = graphicsDevice;
            TextureCache = new Dictionary<string, Texture>();
            SpriteCache = new Dictionary<string, Sprite>();
            SoundEffectCache = new Dictionary<string, SoundEffect>();
            MeshCache = new Dictionary<string, Mesh>();
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

        public void CreateMesh(string meshName, string textureName, List<VertexPositionTexture> verticies) {

            var mesh = new Mesh
            {
                Buffer = new VertexBuffer(_graphicsDevice, typeof(VertexPositionTexture), verticies.Count, BufferUsage.None),
                NumTriangles = verticies.Count / 3,
                MeshType = MeshType.Texture,
                TextureName = textureName
            };

            mesh.Buffer.SetData<VertexPositionTexture>(verticies.ToArray());

            MeshCache[meshName] = mesh;
        }

        public void CreateMesh(string meshName, Color color, List<VertexPosition> verticies)
        {

            var mesh = new Mesh
            {
                Buffer = new VertexBuffer(_graphicsDevice, typeof(VertexPositionColor), verticies.Count, BufferUsage.None),
                NumTriangles = verticies.Count / 3,
                MeshType = MeshType.Color
            };

            mesh.Buffer.SetData<VertexPositionColor>(verticies.Select(v=> new VertexPositionColor(v.Position, color)).ToArray());


            MeshCache[meshName] = mesh;
        }

        public void CreateTexture(string name, int width, int height)
        {
            var newTexture = new Texture(_graphicsDevice, width, height);
            TextureCache[name] = newTexture;
        }

        public void LoadSprite(string spriteName, string path)
        {
            var sprite = Newtonsoft.Json.JsonConvert.DeserializeObject<Sprite>(System.IO.File.ReadAllText(path));
            SpriteCache[spriteName] = sprite;
        }

        public void LoadTexture(string name, string path)
        {
            using (var img = System.Drawing.Image.FromFile(System.IO.Path.GetFullPath(path)))
            {

                TextureCache[name] = new Texture(_graphicsDevice, img.Width, img.Height);
                

                using (var bmp = new System.Drawing.Bitmap(img))
                {

                    for (int y = 0; y < img.Height; y++)
                    {
                        for (int x = 0; x < img.Width; x++)
                        {
                            var c = bmp.GetPixel(x, y);
                            SetPixel(name, x, y, new Color(c.R,c.G,c.B,c.A));
                        }
                    }

                }
            }

            TextureCache[name].Update();
        }

        public void SetPixel(string textureName, int x, int y, Color color)
        {
            var t = TextureCache[textureName];

            t.SetPixel(x, y, color);
        }

        public void UpdateTextures()
        {
            foreach (var t in TextureCache.Values.Where(x => x.IsDirty)) {
                t.Update();
            }
        }

        public void LoadTextureFromResource(string name, string key) {
          
            using (var img = (System.Drawing.Image)(Resources.ResourceManager.GetObject(key)))
            {

                TextureCache[name] = new Texture(_graphicsDevice, img.Width, img.Height);


                using (var bmp = new System.Drawing.Bitmap(img))
                {

                    for (int y = 0; y < img.Height; y++)
                    {
                        for (int x = 0; x < img.Width; x++)
                        {
                            var c = bmp.GetPixel(x, y);
                            SetPixel(name, x, y, new Color(c.R, c.G, c.B, c.A));
                        }
                    }

                }
            }

            TextureCache[name].Update();


        }

        public void LoadAudio(string name, string path)
        {
            SoundEffectCache[name] = SoundEffect.FromFile(path);
        }
    }
}
