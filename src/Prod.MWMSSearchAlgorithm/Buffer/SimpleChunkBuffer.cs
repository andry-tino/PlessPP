/**
 * SimpleChunkBuffer.cs
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

namespace PLessPP.AI.MWMSSearchAlgorithm
{
    using System;
    using System.Linq;
    using System.Collections.Concurrent;

    using PLessPP.Data;

    /// <summary>
    /// 
    /// </summary>
    public class SimpleChunkBuffer : IChunkBuffer
    {
        private readonly int secondsOfData;
        private readonly int itemsPerSecond;

        private readonly ConcurrentQueue<Point> dataPoints = new ConcurrentQueue<Point>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleChunkBuffer"/> class.
        /// </summary>
        public SimpleChunkBuffer(int secondsOfData = 4, int itemsPerSecond = 64)
        {
            this.secondsOfData = secondsOfData;
            this.itemsPerSecond = itemsPerSecond;
        }

        /// <summary>
        /// Gets the cunk, namely saves the current state of the queue and sends it to the caller.
        /// </summary>
        /// <returns></returns>
        public Point[] GetCurrentChunk()
        {
            if (this.dataPoints.Count() < ItemsInChunk)
            {
                // Not enough data yet
                return null;
            }

            Console.WriteLine($"Chunk requested at {DateTime.Now}");

            lock (this.dataPoints)
            {
                return this.dataPoints.ToArray();
            }
        }

        /// <summary>
        /// Clears the buffer.
        /// </summary>
        public void ClearBuffer()
        {
            lock (this.dataPoints)
            {
                Point toChug;
                while (this.dataPoints.TryDequeue(out toChug));
            }
        }

        /// <summary>
        /// Removes data from the queue.
        /// </summary>
        /// <param name="datapoint"></param>
        public void EnqueueData(Point datapoint)
        {
            this.dataPoints.Enqueue(datapoint);

            // Throw out old data
            if (this.dataPoints.Count > ItemsInChunk)
            {
                Point toChug;
                this.dataPoints.TryDequeue(out toChug);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private int ItemsInChunk
        {
            get { return this.secondsOfData * this.itemsPerSecond; }
        }
    }
}
