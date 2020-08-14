using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Interfaces.Context
{
    public interface IScriptContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="initAction"></param>
        void setInit(Action initAction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="render2dAction"></param>
        void setRender2d(Action<double, IGraphicsContext2d> render2dAction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="render3dAction"></param>
        void setRender3d(Action<double, IGraphicsContext3d> render3dAction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processAction"></param>
        void setProcess(Action<double> processAction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        void loadTexture(string name, string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void createTexture(string name, int width, int height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textureName"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        void setPixel(string textureName, int x, int y, byte r, byte g, byte b, byte a);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textureName"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        void setPixel(string textureName, int x, int y, byte r, byte g, byte b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textureName"></param>
        void refreshTexture(string textureName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        void loadSpriteSheet(string name, string path, int cols, int rows);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="textureName"></param>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        void setSpriteSheet(string name, string textureName, int cols, int rows);

    }
}
