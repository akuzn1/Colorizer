using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Core.Converters.PointTransformers
{
    public interface IPointTransformer
    {
        unsafe void Transform(byte* sourcePointer, byte* resultPointer);
    }
}
