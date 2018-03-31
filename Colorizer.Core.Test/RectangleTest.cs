using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Colorizer.Core.Test
{
    [TestClass]
    public class RectangleTest
    {
        [TestMethod]
        public void ValidatePositionCalculations()
        {
            Rectangle rect = new Rectangle { X = 10, Y = 20, Width = 100, Height = 50 };
            Assert.AreEqual(10, rect.Left);
            Assert.AreEqual(20, rect.Top);
            Assert.AreEqual(110, rect.Right);
            Assert.AreEqual(70, rect.Bottom);
        }
    }
}
