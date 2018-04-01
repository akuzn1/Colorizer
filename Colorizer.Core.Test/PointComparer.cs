using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colorizer.Core.Test
{
    internal static class PointComparer
    {
        public static unsafe bool AreEqual(byte* arg1, byte* arg2)
        {
            return arg1[0] == arg2[0] && arg1[1] == arg2[1] && arg1[2] == arg2[2] && arg1[3] == arg2[3];
        }
        public static bool AreEqual(byte[] arg1, byte[] arg2)
        {
            return arg1[0] == arg2[0] && arg1[1] == arg2[1] && arg1[2] == arg2[2] && arg1[3] == arg2[3];
        }
    }
}
