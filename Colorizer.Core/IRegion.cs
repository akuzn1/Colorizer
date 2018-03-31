namespace Colorizer.Core
{
    public interface IRegion
    {
        bool IsInRegion(int x, int y);
        Rectangle GetDescribedRectangle();
    }
}
