/**
 * AbsoluteDifferencePointDistanceCalculator.cs
 * 
 * PLessPP - Copyright (C) 2016
 * Andrea Tino, Constantin Daniil, Jeroen Rietveld, 
 * Liansheng Hua, Nikola Kukrika, Sam van Lieshout
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace PLessPP.Data
{
    using System;

    /// <summary>
    /// 
    /// </summary>
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
