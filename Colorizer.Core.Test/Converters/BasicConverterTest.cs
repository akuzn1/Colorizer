using System;
using System.Drawing;
using Colorizer.Core.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Colorizer.Core.Test.Converters
{
    [TestClass]
    public class BasicConverterTest
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
        public void ValidateBasicConverter()
        {
            var bitmap = BasicInitBitmap();

            BasicConverter converter = new BasicConverter
            {
                SourceColor = Color.FromArgb(255, 255, 0, 0),
                DestinationColor = Color.FromArgb(255, 0, 0, 255)
            };

            Bitmap result = converter.Convert(bitmap);

            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255).ToArgb(), result.GetPixel(0, 0).ToArgb());
            Assert.AreEqual(Color.FromArgb(255, 0, 255, 0).ToArgb(), result.GetPixel(0, 1).ToArgb());
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255).ToArgb(), result.GetPixel(1, 0).ToArgb());
            Assert.AreEqual(Color.FromArgb(0, 0, 0, 0).ToArgb(), result.GetPixel(1, 1).ToArgb());
        }

        [TestMethod]
        public void ValidateEmptySourceColor()
        {
            var bitmap = BasicInitBitmap();
            BasicConverter converter = new BasicConverter
            {
                // SourceColor is empty
                DestinationColor = Color.FromArgb(255, 0, 0, 255)
            };

            Bitmap result = converter.Convert(bitmap);

            Assert.AreEqual(Color.FromArgb(255, 255, 0, 0).ToArgb(), result.GetPixel(0, 0).ToArgb());
            Assert.AreEqual(Color.FromArgb(255, 0, 255, 0).ToArgb(), result.GetPixel(0, 1).ToArgb());
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255).ToArgb(), result.GetPixel(1, 0).ToArgb());
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255).ToArgb(), result.GetPixel(1, 1).ToArgb());
        }

        [TestMethod]
        public void ValidateEmptyDestinationColor()
        {
            var bitmap = BasicInitBitmap();

            BasicConverter converter = new BasicConverter
            {
                SourceColor = Color.FromArgb(255, 255, 0, 0),
                // DestinationColor is empty
            };

            Bitmap result = converter.Convert(bitmap);

            Assert.AreEqual(Color.FromArgb(0, 0, 0, 0).ToArgb(), result.GetPixel(0, 0).ToArgb());
            Assert.AreEqual(Color.FromArgb(255, 0, 255, 0).ToArgb(), result.GetPixel(0, 1).ToArgb());
            Assert.AreEqual(Color.FromArgb(255, 0, 0, 255).ToArgb(), result.GetPixel(1, 0).ToArgb());
            Assert.AreEqual(Color.FromArgb(0, 0, 0, 0).ToArgb(), result.GetPixel(1, 1).ToArgb());
        }

    }
}
