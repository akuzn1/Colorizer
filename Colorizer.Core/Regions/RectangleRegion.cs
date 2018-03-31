using System;

namespace Colorizer.Core.Regions
{
    public class RectangleRegion : IRegion
    {
        public RectangleRegion()
        {
            _rect = new Rectangle();
        }

        Rectangle _rect;

        public RectangleRegion(int x1, int y1, int x2, int y2)
        {
            _rect = new Rectangle
            {
                X = Math.Min(x1, x2),
                Y = Math.Min(y1, y2),
                Width = Math.Max(x1, x2) - Math.Min(x1, x2),
                Height = Math.Max(y1, y2) - Math.Min(y1, y2)
            };

        }

        public int X { get { return _rect.X; } set { _rect.X = value; } }
        public int Y { get { return _rect.Y; } set { _rect.Y = value; } }
        public int Width { get { return _rect.Width; } set { _rect.Width = value; } }
        public int Height { get { return _rect.Height; } set { _rect.Height = value; } }

        public Rectangle GetDescribedRectangle()
        {
            return _rect;
        }

        public bool IsInRegion(int x, int y)
        {
            return x >= _rect.X && x <= _rect.X + _rect.Width && y >= _rect.Y && y <= _rect.Y + _rect.Height;
        }
    }
}