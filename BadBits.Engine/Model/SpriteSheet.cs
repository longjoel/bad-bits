using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace BadBits.Engine.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class SpriteSheet
    {
        public string Texture { get; private set; }
        public Dictionary<string, Rectangle> Cells { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="texture"></param>
        public SpriteSheet(string texture)
        {
            Texture = texture;
            Cells = new Dictionary<string, Rectangle>();
        }

    }
}
