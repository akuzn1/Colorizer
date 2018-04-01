using Colorizer.Core.Converters.PointTransformers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Colorizer.Core.Converters
{
    /// <summary>
    /// The universal converter for single point based only on it's parameters.
    /// </summary>
    public class PointsConverter : IBitmapConverter
    {
        public PointsConverter(IPointTransformer transformer)
        {
            Transformer = transformer;
        }

        public IPointTransformer Transformer { get; set; } 

        public unsafe Bitmap Convert(Bitmap bitmapSource)
        {
            if (Transformer == null)
                throw new NullReferenceException("Can't use Convert when Transformer is null. Set it before using this function.");

            Bitmap result = new Bitmap(bitmapSource);
            BitmapData sourceData = bitmapSource.LockBits(new System.Drawing.Rectangle(0, 0, bitmapSource.Width, bitmapSource.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            BitmapData resultData = result.LockBits(new System.Drawing.Rectangle(0, 0, bitmapSource.Width, bitmapSource.Height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);
            try
            {
                int length = bitmapSource.Width * bitmapSource.Height;
                byte* sourcePointer = (byte*)(sourceData.Scan0);
                byte* resultPointer = (byte*)(resultData.Scan0);
                for (int i = 0; i < length; i++)
                {
                    Transformer.Transform(sourcePointer, resultPointer);

                    sourcePointer += 4;
                    resultPointer += 4;
                }
            }
            finally
            {
                bitmapSource.UnlockBits(sourceData);
                result.UnlockBits(resultData);
            }
            return result;
        }
    }
}