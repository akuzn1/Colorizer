using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Core.Converters.PointTransformers
{
    public class MultipleColorsPointTransformer : IPointTransformer
    {
        public MultipleColorsPointTransformer(Dictionary<Color, Color> colors, double precision)
        {
            Colors = colors;
            Precision = precision;
        }
        public Dictionary<Color, Color> Colors { get; set; }
        public double Precision { get; set; }
        public unsafe void Transform(byte* sourcePointer, byte* resultPointer)
        {
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
                    break;
                }
            }
        }
    }
}
