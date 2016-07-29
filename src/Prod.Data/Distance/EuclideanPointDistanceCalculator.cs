﻿/**
 * EuclideanPointDistanceCalculator.cs
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