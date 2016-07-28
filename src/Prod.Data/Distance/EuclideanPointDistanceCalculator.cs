/// <summary>
/// EuclideanPointDistanceCalculator.cs
/// </summary>

namespace PLessPP.Data
{
    using System;

    public class EuclideanPointDistanceCalculator : IPointDistanceCalculator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        /// <remarks>
        /// Currently ignoring gyro values.
        /// </remarks>
        public double GetDistance(Point point1, Point point2)
        {
            return
                Math.Pow(point1.AccelerationX - point2.AccelerationX, 2) +
                Math.Pow(point1.AccelerationY - point2.AccelerationY, 2) +
                Math.Pow(point1.AccelerationZ - point2.AccelerationZ, 2);
        }
    }
}
