using Colorizer.Core.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Colorizer.Core.Test.Regions
{
    [TestClass]
    public class RectangleRegionTest
    {
        [TestMethod]
        public void ValidateDefaultConstructor()
        {
            RectangleRegion region = new RectangleRegion();
            Rectangle rect = region.GetDescribedRectangle();
            Assert.IsNotNull(rect);
            Assert.AreEqual(0, rect.X);
            Assert.AreEqual(0, rect.Y);
            Assert.AreEqual(0, rect.Width);
            Assert.AreEqual(0, rect.Height);
        }
        [TestMethod]
        public void ValidatePropertiesAssign()
        {
            RectangleRegion region = new RectangleRegion
            {
                X = 10,
                Y = 20,
                Width = 100,
                Height = 50
            };
            Rectangle rect = region.GetDescribedRectangle();
            Assert.AreEqual(10, rect.X);
            Assert.AreEqual(20, rect.Y);
            Assert.AreEqual(100, rect.Width);
            Assert.AreEqual(50, rect.Height);
        }
        [TestMethod]
        public void ValidateCreateRegionByLeftTopRightBottomPoints()
        {
            RectangleRegion region = new RectangleRegion(10, 20, 110, 70);
            Rectangle rect = region.GetDescribedRectangle();
            Assert.AreEqual(10, rect.X);
            Assert.AreEqual(20, rect.Y);
            Assert.AreEqual(100, rect.Width);
            Assert.AreEqual(50, rect.Height);
        }
        [TestMethod]
        public void ValidateCreateRegionByRightBottomLeftTopPoints()
        {
            RectangleRegion region = new RectangleRegion(110, 70, 10, 20);
            Rectangle rect = region.GetDescribedRectangle();
            Assert.AreEqual(10, rect.X);
            Assert.AreEqual(20, rect.Y);
            Assert.AreEqual(100, rect.Width);
            Assert.AreEqual(50, rect.Height);
        }
        [TestMethod]
        public void ValidateCreateRegionByLeftBottomRightTopPoints()
        {
            RectangleRegion region = new RectangleRegion(10, 70, 110, 20);
            Rectangle rect = region.GetDescribedRectangle();
            Assert.AreEqual(10, rect.X);
            Assert.AreEqual(20, rect.Y);
            Assert.AreEqual(100, rect.Width);
            Assert.AreEqual(50, rect.Height);
        }
        [TestMethod]
        public void ValidateCreateRegionByRightTopLeftBottomPoints()
        {
            RectangleRegion region = new RectangleRegion(110, 20, 10, 70);
            Rectangle rect = region.GetDescribedRectangle();
            Assert.AreEqual(10, rect.X);
            Assert.AreEqual(20, rect.Y);
            Assert.AreEqual(100, rect.Width);
            Assert.AreEqual(50, rect.Height);
        }
        [TestMethod]
        public void ValidateIsInRegion()
        {
            RectangleRegion region = new RectangleRegion(10, 20, 110, 70);
            Assert.IsTrue(region.IsInRegion(50, 50));
            Assert.IsTrue(region.IsInRegion(10, 20));
            Assert.IsTrue(region.IsInRegion(110, 70));
            Assert.IsFalse(region.IsInRegion(0, 0));
            Assert.IsFalse(region.IsInRegion(20, 10));
            Assert.IsFalse(region.IsInRegion(20, 80));
            Assert.IsFalse(region.IsInRegion(5, 30));
            Assert.IsFalse(region.IsInRegion(120, 30));
        }
    }
}