using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Interfaces.Context
{
    public interface IGraphicsContext2d
    {
        void CreateTexture(string name, int width, int height);
        void LoadTexture(string name, string path);
        void SetPixel(string textureName, int x, int y, byte r, byte g, byte b, byte a);
        void SetPixel(string textureName, int x, int y, byte r, byte g, byte b);
        void Render();

        void DrawSprite(string name, int x, int y, int row, int col);
        void DrawTexture(string name, int[] srcRect, int[] destRect);
    }
}
