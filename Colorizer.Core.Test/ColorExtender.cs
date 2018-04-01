using System.Drawing;

namespace Colorizer.Core.Test
{
    public static class ColorExtender
    {
        public static byte[] ToBGRAByteArray(this Color color)
        {
            byte[] res = new byte[4];
            res[0] = color.B;
            res[1] = color.G;
            res[2] = color.R;
            res[3] = color.A;
            return res;
        }
    }
}
