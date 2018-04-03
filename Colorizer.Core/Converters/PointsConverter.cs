using Colorizer.Core.Converters.PointTransformers;
using Colorizer.Core.Regions;
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
            Regions = new List<IRegion>();
        }

        public IPointTransformer Transformer { get; set; } 

        public List<IRegion> Regions { get; set; }

        public unsafe Bitmap Convert(Bitmap bitmapSource)
        {
            if (Transformer == null)
                throw new NullReferenceException("Can't use Convert when Transformer is null. Set it before using this function.");

            var regions = Regions;
            if(regions == null || regions.Count == 0)
            {
                regions = new List<IRegion> { new RectangleRegion(0,0,bitmapSource.Width, bitmapSource.Height) };
            }

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
                for (int i = 0; i < bitmapSource.Width; i++)
                {
                    for (int j = 0; j < bitmapSource.Height; j++)
                    {
                        bool isInRegion = false;
                        foreach (var region in regions)
                        {
                            if(region.IsInRegion(i,j))
                            {
                                isInRegion = true;
                                break;
                            }
                        }

                        if (isInRegion)
                        {
                            Transformer.Transform(sourcePointer, resultPointer);
                        }

                        sourcePointer += 4;
                        resultPointer += 4;
                    }
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