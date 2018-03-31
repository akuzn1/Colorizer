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

        public UniversalConverter(Color sourceColor, Color destinationColor, double precision)
        {
            SourceColor = sourceColor;
            DestinationColor = destinationColor;
            Precision = precision;
        }

        public Color SourceColor { get; set; }

        public Color DestinationColor { get; set; }

        public double Precision { get; set; }

        public unsafe Bitmap Convert(Bitmap bitmapSource)
        {
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
                    if (*(sourcePointer) >= SourceColor.B - Precision && *(sourcePointer) <= SourceColor.B + Precision &&
                        *(sourcePointer + 1) >= SourceColor.G - Precision && *(sourcePointer + 1) <= SourceColor.G + Precision &&
                        *(sourcePointer + 2) >= SourceColor.R - Precision && *(sourcePointer + 2) <= SourceColor.R + Precision &&
                        *(sourcePointer + 3) >= SourceColor.A - Precision && *(sourcePointer + 3) <= SourceColor.A + Precision)
                    {
                        *resultPointer = DestinationColor.B;
                        *(resultPointer + 1) = DestinationColor.G;
                        *(resultPointer + 2) = DestinationColor.R;
                        *(resultPointer + 3) = DestinationColor.A;
                    }
                    else
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