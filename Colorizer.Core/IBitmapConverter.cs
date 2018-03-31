using System.Drawing;

namespace Colorizer.Core
{
    public interface IBitmapConverter
    {
        Bitmap Convert(Bitmap source);
    }
}