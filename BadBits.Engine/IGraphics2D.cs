namespace BadBits.Engine
{
    public interface IGraphics2D
    {
        void clear();
        void render();
        void setPixel(int x, int y, byte r, byte g, byte b, byte a);
        void setRect(int x, int y, int w, int h, byte r, byte g, byte b, byte a);
    }
}