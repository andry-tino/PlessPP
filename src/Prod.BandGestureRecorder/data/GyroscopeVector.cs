/**
 * GyroscopeVector.cs
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

namespace PLessPP.Device.BandGestureRecorder
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    internal struct GyroscopeVector
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
        public double AngularVelocityX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double AngularVelocityY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double AngularVelocityZ { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{{{this.AccelerationX},{this.AccelerationY},{this.AccelerationZ},{this.AngularVelocityX},{this.AngularVelocityY},{this.AngularVelocityZ}}}";
        }
    }
}
