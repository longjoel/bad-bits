using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BadBits.Engine.Next.Tests.Model.Client
{
    [TestClass]
    public class RectShould
    {
        Jint.Engine _engine;

        [TestInitialize]
        public void Init()
        {
            _engine = new Jint.Engine();
        }

        public BadBits.Engine.Next.Models.Client.Rect GetRect(dynamic r) {

            return new Models.Client.Rect {X = r.X,Y=20,Width=40, Height=50 };

        }

        [TestMethod]
        public void Test()
        {
            _engine.SetValue("test", this);

            _engine.Execute("test.GetRect({X:10,Y:10, Width:10, Height:10})");

            var result = ((Models.Client.Rect)_engine.GetCompletionValue().ToObject()).BackendRect;

           Assert.AreEqual(10, result.X);
        }

    }
}
