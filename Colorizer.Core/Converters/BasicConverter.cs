using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Core.Converters
{
    public class BasicConverter : IBitmapConverter
    {
        public BasicConverter()
        { }
        public Color SourceColor { get; set; }
        public Color DestinationColor { get; set; }
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
                for (int i = 0; i < length; i++ )
                {
                    if (*(sourcePointer) == SourceColor.B && *(sourcePointer + 1) == SourceColor.G && *(sourcePointer + 2) == SourceColor.R)
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