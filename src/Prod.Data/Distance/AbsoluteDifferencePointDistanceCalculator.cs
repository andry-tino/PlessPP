/// <summary>
/// AbsoluteDifferencePointDistanceCalculator.cs
/// </summary>

namespace PLessPP.Data
{
    using System;

    public class AbsoluteDifferencePointDistanceCalculator : IPointDistanceCalculator
    {
        private const double alpha = 0.5;
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
            return alpha * 
                (Math.Abs(point1.AccelerationX - point2.AccelerationX) + 
                Math.Abs(point1.AccelerationY - point2.AccelerationY) + 
                Math.Abs(point1.AccelerationZ - point2.AccelerationZ)) + 
                (1.0 - alpha) * 
                (Math.Abs(point1.GyroX - point2.GyroX) +
                Math.Abs(point1.GyroY - point2.GyroY) +
                Math.Abs(point1.GyroZ - point2.GyroZ));
        }
    }
}
