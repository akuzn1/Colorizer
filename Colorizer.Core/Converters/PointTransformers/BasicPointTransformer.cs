using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Core.Converters.PointTransformers
{
    public class BasicPointTransformer : IPointTransformer
    {
        public BasicPointTransformer(Color sourceColor, Color destinationColor, double precision)
        {
            SourceColor = sourceColor;
            DestinationColor = destinationColor;
            Precision = precision;
        }
        public Color SourceColor { get; set; }
        public Color DestinationColor { get; set; }
        public double Precision { get; set; }
        public unsafe void Transform(byte* sourcePointer, byte* resultPointer)
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
        }
    }
}
