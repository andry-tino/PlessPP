using PLessPP.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLessPP.AI
{
    {

        private const int SecondsOfData = 4;
        private const int ItemsPerSecond = 64;
        private const int ItemsInChunk = SecondsOfData * ItemsPerSecond;

        private readonly ConcurrentQueue<Point> dataPoints = new ConcurrentQueue<Point>();

        public SimpleChunkBuffer()
        {
        }

        public Point[] GetCurrentChunk()
        {
            if (this.dataPoints.Count() < ItemsInChunk)
            {
                return null; // not enough data yet
            }

            lock (this.dataPoints)
            {
                return this.dataPoints.ToArray();
            }
        }

        public void ClearBuffer()
        {
            lock (this.dataPoints)
            {
                Point toChug;
                while (this.dataPoints.TryDequeue(out toChug)) ;
            }
        }

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
    }
}
