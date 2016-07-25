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
    }
}
