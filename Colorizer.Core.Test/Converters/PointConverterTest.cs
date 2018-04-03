using System;
using System.Drawing;
using Colorizer.Core.Converters;
using Colorizer.Core.Converters.PointTransformers;
using Colorizer.Core.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Colorizer.Core.Test.Converters
{
    [TestClass]
    public class PointConverterTest
    {
        private Bitmap BasicInitBitmap()
        {
            var bitmap = new Bitmap(2, 2);
            bitmap.SetPixel(0, 0, Color.FromArgb(255, 255, 0, 0));
            bitmap.SetPixel(0, 1, Color.FromArgb(255, 0, 255, 0));
            bitmap.SetPixel(1, 0, Color.FromArgb(255, 0, 0, 255));
            bitmap.SetPixel(1, 1, Color.FromArgb(0, 0, 0, 0));
            return bitmap;
        }

        [TestMethod]
        public void ValidatePointConverter()
        {
            var bitmap = BasicInitBitmap();

            var transformer = new BasicPointTransformer(Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 0, 0, 255), 5);

            var converter = new PointsConverter(transformer);

            Bitmap result = converter.Convert(bitmap);

            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255).ToArgb(), result.GetPixel(0, 0).ToArgb());
            Assert.AreEqual(Color.FromArgb(255, 0, 255, 0).ToArgb(), result.GetPixel(0, 1).ToArgb());
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255).ToArgb(), result.GetPixel(1, 0).ToArgb());
            Assert.AreEqual(Color.FromArgb(0, 0, 0, 0).ToArgb(), result.GetPixel(1, 1).ToArgb());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ValidateEmptyTransformer()
        {
            var bitmap = BasicInitBitmap();

            var converter = new PointsConverter(null);

            Bitmap result = converter.Convert(bitmap);
        }

        [TestMethod]
        public void ValidateRegion()
        {
            var bitmap = new Bitmap(2, 2);
            bitmap.SetPixel(0, 0, Color.FromArgb(255, 255, 0, 0));
            bitmap.SetPixel(0, 1, Color.FromArgb(255, 255, 0, 0));
            bitmap.SetPixel(1, 0, Color.FromArgb(255, 255, 0, 0));
            bitmap.SetPixel(1, 1, Color.FromArgb(255, 255, 0, 0));

            var transformer = new BasicPointTransformer(Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 0, 0, 255), 5);

            var converter = new PointsConverter(transformer);
            converter.Regions.Add(new RectangleRegion(0, 0, 1, 0));

            Bitmap result = converter.Convert(bitmap);

            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255).ToArgb(), result.GetPixel(0, 0).ToArgb());
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255).ToArgb(), result.GetPixel(0, 1).ToArgb());
            Assert.AreEqual(Color.FromArgb(255, 255, 0, 0).ToArgb(), result.GetPixel(1, 0).ToArgb());
            Assert.AreEqual(Color.FromArgb(255, 255, 0, 0).ToArgb(), result.GetPixel(1, 1).ToArgb());
        }

        [TestMethod]
        public void ValidateMultipleRegions()
        {
            var bitmap = new Bitmap(2, 2);
            bitmap.SetPixel(0, 0, Color.FromArgb(255, 255, 0, 0));
            bitmap.SetPixel(0, 1, Color.FromArgb(255, 255, 0, 0));
            bitmap.SetPixel(1, 0, Color.FromArgb(255, 255, 0, 0));
            bitmap.SetPixel(1, 1, Color.FromArgb(255, 255, 0, 0));

            var transformer = new BasicPointTransformer(Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 0, 0, 255), 5);

            var converter = new PointsConverter(transformer);
            converter.Regions.Add(new RectangleRegion(0, 0, 0, 0));
            converter.Regions.Add(new RectangleRegion(1, 1, 1, 1));

            Bitmap result = converter.Convert(bitmap);

            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255).ToArgb(), result.GetPixel(0, 0).ToArgb());
            Assert.AreEqual(Color.FromArgb(255, 255, 0, 0).ToArgb(), result.GetPixel(0, 1).ToArgb());
            Assert.AreEqual(Color.FromArgb(255, 255, 0, 0).ToArgb(), result.GetPixel(1, 0).ToArgb());
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255).ToArgb(), result.GetPixel(1, 1).ToArgb());
        }
    }
}