
namespace BadBits.Engine.Next.Models.Client
{
    public class VecPositionTexture
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
             
        public double U { get; set; }
        public double V { get; set; }

        public Microsoft.Xna.Framework.Graphics.VertexPositionTexture VertexPositionColor => new Microsoft.Xna.Framework.Graphics.VertexPositionTexture(
            new Microsoft.Xna.Framework.Vector3((float)X,
                (float)Y,
                (float)Z), new Microsoft.Xna.Framework.Vector2(
                    (float)U,
                    (float)V));
    }
}
