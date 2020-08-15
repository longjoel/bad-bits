namespace BadBits.Engine.Interfaces.Context
{
    public interface IGraphicsContext2d
    {
        void CreateTexture(string name, int width, int height);

        void LoadTexture(string name, string path);
        void LoadSpriteSheet(string name, string path, int rows, int cols);


        void SetPixel(string textureName, int x, int y, byte r, byte g, byte b, byte a);
        void SetPixel(string textureName, int x, int y, byte r, byte g, byte b);
        void Render();

        void DrawSprite(string name, int x, int y, int row, int col);
        void DrawTexture(string name, int[] srcRect, int[] destRect);

        void SetSpriteSheet(string name,  int rows, int cols);
    }
}
