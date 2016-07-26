/// <summary>
/// IPointDistanceCalculator.cs
/// </summary>

namespace PLessPP.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPointDistanceCalculator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        double GetDistance(Point point1, Point point2);
    }
}
