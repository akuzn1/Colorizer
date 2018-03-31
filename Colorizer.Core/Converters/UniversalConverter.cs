using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Colorizer.Core.Converters
{
    /// <summary>
    /// The universal converter for most typically cases.
    /// </summary>
    public class UniversalConverter : IBitmapConverter
    {
        public UniversalConverter() { }

        public UniversalConverter(Dictionary<Color, Color> colors, double precision)
        {
            Colors = colors;
            Precision = precision;
        }

        public Dictionary<Color, Color> Colors { get; set; }

        public double Precision { get; set; }

        public unsafe Bitmap Convert(Bitmap bitmapSource)
        {
            if (Colors == null)
                throw new NullReferenceException("Can't use Convert when Colors is null. Set it before using this function.");

            Bitmap result = new Bitmap(bitmapSource.Width, bitmapSource.Height);
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
                    bool isColorized = false;
                    foreach (var color in Colors.Keys)
                    {                        
                        if (*(sourcePointer) >= color.B - Precision && *(sourcePointer) <= color.B + Precision &&
                            *(sourcePointer + 1) >= color.G - Precision && *(sourcePointer + 1) <= color.G + Precision &&
                            *(sourcePointer + 2) >= color.R - Precision && *(sourcePointer + 2) <= color.R + Precision &&
                            *(sourcePointer + 3) >= color.A - Precision && *(sourcePointer + 3) <= color.A + Precision)
                        {
                            var destinationColor = Colors[color];
                            *resultPointer = destinationColor.B;
                            *(resultPointer + 1) = destinationColor.G;
                            *(resultPointer + 2) = destinationColor.R;
                            *(resultPointer + 3) = destinationColor.A;
                            isColorized = true;
                            break;
                        }
                    }
                    if(!isColorized)
                    {
                        *resultPointer = *sourcePointer;
                        *(resultPointer + 1) = *(sourcePointer + 1);
                        *(resultPointer + 2) = *(sourcePointer + 2);
                        *(resultPointer + 3) = *(sourcePointer + 3);
                    }
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