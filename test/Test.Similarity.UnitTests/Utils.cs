/// <summary>
/// Utils.cs
/// </summary>

namespace PLessPP.Testing.Similarity
{
    using System;

    using PLessPP.Data;

    internal static class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceleration"></param>
        /// <returns></returns>
        public static Point BuildPoint(double acceleration)
        {
            return new Point(acceleration, acceleration, acceleration);
        }
    }
}
