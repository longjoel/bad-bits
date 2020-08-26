
namespace BadBits.Engine.Next.Models.Client
{
    public class VecPositionTexture
    {
        public Jint.Native.JsValue X { get; set; }
        public Jint.Native.JsValue Y { get; set; }
        public Jint.Native.JsValue Z { get; set; }

        public Jint.Native.JsValue U { get; set; }
        public Jint.Native.JsValue V { get; set; }

        public Microsoft.Xna.Framework.Graphics.VertexPositionTexture VertexPositionColor => new Microsoft.Xna.Framework.Graphics.VertexPositionTexture(
            new Microsoft.Xna.Framework.Vector3((float)X.AsNumber(),
                (float)Y.AsNumber(),
                (float)Z.AsNumber()), new Microsoft.Xna.Framework.Vector2(
                    (float)U.AsNumber(),
                    (float)V.AsNumber()));
    }
}
