/// <summary>
/// AbsoluteDifferencePointDistanceCalculator.cs
/// </summary>

namespace PLessPP.Data
{
    using System;

    public class AbsoluteDifferencePointDistanceCalculator : IPointDistanceCalculator
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
                Math.Abs(point1.AccelerationX - point2.AccelerationX) +
                Math.Abs(point1.AccelerationY - point2.AccelerationY) +
                Math.Abs(point1.AccelerationZ - point2.AccelerationZ);
        }
    }
}
