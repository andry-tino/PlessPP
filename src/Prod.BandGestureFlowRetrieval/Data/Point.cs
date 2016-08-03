/**
 * Point.cs
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

namespace PLessPP.Device.BandGestureFlowRetrieval
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public class Point
    {
        public double AcceleartionX { get; private set; }
        public double AcceleartionY { get; private set; }
        public double AcceleartionZ { get; private set; }
        public double AngularVelocityX { get; private set; }
        public double AngularVelocityY { get; private set; }
        public double AngularVelocityZ { get; private set; }
        public long Ticks { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class.
        /// </summary>
        /// <param name="acceleartionX"></param>
        /// <param name="acceleartionY"></param>
        /// <param name="acceleartionZ"></param>
        /// <param name="angularVelocityX"></param>
        /// <param name="angularVelocityY"></param>
        /// <param name="angularVelocityZ"></param>
        /// <param name="ticks"></param>
        public Point(
            double acceleartionX,
            double acceleartionY,
            double acceleartionZ,
            double angularVelocityX,
            double angularVelocityY,
            double angularVelocityZ,
            long ticks)
        {
            this.AcceleartionX = acceleartionX;
            this.AcceleartionY = acceleartionY;
            this.AcceleartionZ = acceleartionZ;
            this.AngularVelocityX = angularVelocityX;
            this.AngularVelocityY = angularVelocityY;
            this.AngularVelocityZ = angularVelocityZ;
            this.Ticks = ticks;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return
                $"{this.AcceleartionX},{this.AcceleartionY},{this.AcceleartionZ},{this.AngularVelocityX},{this.AngularVelocityY},{this.AngularVelocityZ},{this.Ticks}";
        }
    }
}
