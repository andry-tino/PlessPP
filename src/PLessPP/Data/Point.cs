/// <summary>
/// 
/// </summary>

namespace PLessPP.Data
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// 
        /// </summary>
        public double AccelerationX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double AccelerationY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double AccelerationZ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double GyroX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double GyroY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double GyroZ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UInt64 Timestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accX"></param>
        /// <param name="accY"></param>
        /// <param name="accZ"></param>
        /// <param name="gyrX"></param>
        /// <param name="gyrY"></param>
        /// <param name="gyrZ"></param>
        /// <param name="tstamp"></param>
        public Point(double accX, double accY, double accZ, double gyrX, double gyrY, double gyrZ, UInt64 tstamp)
        {
            this.AccelerationX = accX;
            this.AccelerationY = accY;
            this.AccelerationZ = accZ;
            this.GyroX = gyrX;
            this.GyroY = gyrY;
            this.GyroZ = gyrZ;
            this.Timestamp = tstamp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accX"></param>
        /// <param name="accY"></param>
        /// <param name="accZ"></param>
        public Point(double accX, double accY, double accZ) : this(accX, accY, accZ, 0, 0, 0, 0)
        {
        }

        public static Point operator +(Point point1, Point point2)
        {
            return new Point(
                point1.AccelerationX + point2.AccelerationX,
                point1.AccelerationY + point2.AccelerationY,
                point1.AccelerationZ + point2.AccelerationZ,
                point1.GyroX + point2.GyroX,
                point1.GyroY + point2.GyroY,
                point1.GyroZ + point2.GyroZ,
                0);
        }

        public static Point operator -(Point point1, Point point2)
        {
            return new Point(
                point1.AccelerationX - point2.AccelerationX,
                point1.AccelerationY - point2.AccelerationY,
                point1.AccelerationZ - point2.AccelerationZ,
                point1.GyroX - point2.GyroX,
                point1.GyroY - point2.GyroY,
                point1.GyroZ - point2.GyroZ,
                point1.Timestamp);
        }

        public static Point operator *(Point point1, Point point2)
        {
            return new Point(
                point1.AccelerationX * point2.AccelerationX,
                point1.AccelerationY * point2.AccelerationY,
                point1.AccelerationZ * point2.AccelerationZ,
                point1.GyroX * point2.GyroX,
                point1.GyroY * point2.GyroY,
                point1.GyroZ * point2.GyroZ,
                0);
        }

        public static Point operator /(Point point1, Point point2)
        {
            return new Point(
                point1.AccelerationX / point2.AccelerationX,
                point1.AccelerationY / point2.AccelerationY,
                point1.AccelerationZ / point2.AccelerationZ,
                point1.GyroX / point2.GyroX,
                point1.GyroY / point2.GyroY,
                point1.GyroZ / point2.GyroZ,
                point1.Timestamp);
        }

        public static Point operator /(Point point1, int n)
        {
            return new Point(
                point1.AccelerationX / n,
                point1.AccelerationY / n,
                point1.AccelerationZ / n,
                point1.GyroX / n,
                point1.GyroY / n,
                point1.GyroZ / n,
                point1.Timestamp);
        }
    }
}
