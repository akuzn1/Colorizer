using System;
using System.Collections.Generic;
using System.Drawing;
using Colorizer.Core.Converters.PointTransformers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Colorizer.Core.Test.Converters.PointTransformers
{
    [TestClass]
    public class MultipleColorsPointTransformerTest
    {
        [TestMethod]
        public unsafe void ValidateTransformerWithFirstValidColor()
        {
            MultipleColorsPointTransformer transformer = new MultipleColorsPointTransformer(
                colors: new Dictionary<Color, Color>
                        {
                            { Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 0, 0, 255) },
                            { Color.FromArgb(255, 0, 255, 0), Color.FromArgb(255, 255, 0, 255) },
                        },
                precision: 5);

            var source = Color.FromArgb(255, 255, 0, 0).ToBGRAByteArray();
            var res = new byte[4];
            fixed (byte* sourcePointer = &source[0])
            {
                fixed (byte* resPointer = &res[0])
                {
                    transformer.Transform(sourcePointer, resPointer);
                }
            }
            Assert.IsTrue(PointComparer.AreEqual(res, Color.FromArgb(255, 0, 0, 255).ToBGRAByteArray()));
        }

        [TestMethod]
        public unsafe void ValidateTransformerWithSecondValidColor()
        {
            MultipleColorsPointTransformer transformer = new MultipleColorsPointTransformer(
                colors: new Dictionary<Color, Color>
                        {
                            { Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 0, 0, 255) },
                            { Color.FromArgb(255, 0, 255, 0), Color.FromArgb(255, 255, 0, 255) },
                        },
                precision: 5);

            var source = Color.FromArgb(255, 0, 255, 0).ToBGRAByteArray();
            var res = new byte[4];
            fixed (byte* sourcePointer = &source[0])
            {
                fixed (byte* resPointer = &res[0])
                {
                    transformer.Transform(sourcePointer, resPointer);
                }
            }
            Assert.IsTrue(PointComparer.AreEqual(res, Color.FromArgb(255, 255, 0, 255).ToBGRAByteArray()));
        }

        [TestMethod]
        public unsafe void ValidateConcurenceColorsWithValidColor()
        {
            MultipleColorsPointTransformer transformer = new MultipleColorsPointTransformer(
                colors: new Dictionary<Color, Color>
                        {
                            { Color.FromArgb(255, 230, 0, 0), Color.FromArgb(255, 0, 0, 255) },
                            { Color.FromArgb(255, 240, 0, 0), Color.FromArgb(255, 255, 0, 255) },
                        },
                precision: 10);

            var source = Color.FromArgb(255, 235, 0, 0).ToBGRAByteArray();
            var res = new byte[4];
            fixed (byte* sourcePointer = &source[0])
            {
                fixed (byte* resPointer = &res[0])
                {
                    transformer.Transform(sourcePointer, resPointer);
                }
            }
            Assert.IsTrue(PointComparer.AreEqual(res, Color.FromArgb(255, 0, 0, 255).ToBGRAByteArray()));
        }

        [TestMethod]
        public unsafe void ValidateTransformerWithInvalidColor()
        {
            MultipleColorsPointTransformer transformer = new MultipleColorsPointTransformer(
                colors: new Dictionary<Color, Color>
                        {
                            { Color.FromArgb(255, 255, 0, 0), Color.FromArgb(255, 0, 0, 255) },
                            { Color.FromArgb(255, 0, 255, 0), Color.FromArgb(255, 255, 0, 255) },
                        },
                precision: 5);

            var source = Color.FromArgb(255, 0, 255, 255).ToBGRAByteArray();
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
    }
}
