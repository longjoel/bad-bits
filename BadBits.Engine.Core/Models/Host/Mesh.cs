using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadBits.Engine.Models.Host
{
    public enum MeshType { Color,Texture }
    public class Mesh
    {
        public VertexBuffer Buffer { get; set; }
        public int NumTriangles { get; set; }
        public string TextureName { get; set; }
        public MeshType MeshType { get; set; }
    }
}
