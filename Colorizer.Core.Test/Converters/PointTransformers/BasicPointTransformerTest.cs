using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Colorizer.Core.Converters.PointTransformers;
using System.Drawing;

namespace Colorizer.Core.Test.Converters.PointTransformers
{
    [TestClass]
    public class BasicPointTransformerTest
    {
        [TestMethod]
        public unsafe void ValidateTransformerWithValidColor()
        {
            BasicPointTransformer transformer = new BasicPointTransformer(Color.FromArgb(255,255,0,0), Color.FromArgb(255,0,0,255), 5);
            var source = Color.FromArgb(255, 255, 0, 0).ToBGRAByteArray();
            var res = new byte[4];
            fixed (byte* sourcePointer = &source[0])
            {
                fixed(byte* resPointer = &res[0])
                {
                    transformer.Transform(sourcePointer, resPointer);
                }
            }
            Assert.IsTrue(PointComparer.AreEqual(res, new byte[] { 255, 0, 0, 255 }));
        }

        [TestMethod]
        public unsafe void ValidateTransformerWithInvalidColor()
        {
            BasicPointTransformer transformer = new BasicPointTransformer(Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 0, 0, 255), 5);
            var source = Color.FromArgb(255, 0, 255, 0).ToBGRAByteArray();
            var res = new byte[4];
            fixed (byte* sourcePointer = &source[0])
            {
                fixed (byte* resPointer = &res[0])
                {
                    transformer.Transform(sourcePointer, resPointer);
                }
            }
            Assert.IsTrue(PointComparer.AreEqual(res, new byte[] { 0, 0, 0, 0 }));
        }

        [TestMethod]
        public unsafe void ValidatePrecision()
        {
            BasicPointTransformer transformer = new BasicPointTransformer(Color.FromArgb(250, 255, 0, 0), Color.FromArgb(255, 0, 0, 255), 5);
            var source = Color.FromArgb(255, 255, 0, 0).ToBGRAByteArray();
            var res = new byte[4];
            fixed (byte* sourcePointer = &source[0])
            {
                fixed (byte* resPointer = &res[0])
                {
                    transformer.Transform(sourcePointer, resPointer);
                }
            }
            Assert.IsTrue(PointComparer.AreEqual(res, new byte[] { 255, 0, 0, 255 }));
        }
    }
}
