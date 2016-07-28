/// <summary>
/// SimpleChunkBuffer.cs
/// </summary>

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
