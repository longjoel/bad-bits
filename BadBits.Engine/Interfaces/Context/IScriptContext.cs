﻿using BadBits.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadBits.Engine.Interfaces.Context
{
   
    public interface IScriptContext
    {
        Action InitAction { get; }
        Action<double> ProcessAction { get; }
        Action<double> Render2dAction { get; }
        Action<double> Render3dAction { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="initAction"></param>
        void setInit(Action initAction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="render2dAction"></param>
        void setRender2d(Action<double> render2dAction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="render3dAction"></param>
        void setRender3d(Action<double> render3dAction);

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
        void setPixelTransparent(string textureName, int x, int y, int r, int g, int b, int a);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textureName"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        void setPixel(string textureName, int x, int y, int r, int g, int b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        void loadSpriteSheet(string name, string path, int rows, int cols);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        void setSpriteSheet(string name,  int rows, int cols);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="srcRect"></param>
        /// <param name="destRect"></param>
        void drawTexture(string name, int[] srcRect, int[] destRect);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        void drawSprite(string name, int x, int y, int row, int col);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Model.InputState pollInput();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        void makeTransparent(string name, int r, int g, int b);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        SpriteAttribs getSpriteAttribs(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        TextureAttribs getTextureAttribs(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        void logInfo(string text);

    }
}
