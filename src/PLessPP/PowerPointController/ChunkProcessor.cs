using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPointController
{
    public class ChunkProcessor
    {
        private readonly ConcurrentQueue<DataPoint> dataPoints = new ConcurrentQueue<DataPoint>();
        private DateTime lastMovement = DateTime.Now;

        private const int SecondsOfData = 4;
        private const int ItemsPerSecond = 64;
        private const int ItemsInChunk = SecondsOfData * ItemsPerSecond;

        public ChunkProcessor()
        {
        }

        /// <summary>
        ///     TODO get from Andrea
        /// </summary>
        /// <param name="points"></param>
        public bool ProcessChunk(DataPoint[] points)
        {
            // This commented code is for when you want to write sample data to a file
            /*
            StorageFile outputFile =
                (await
                    folder.CreateFileAsync(
                        @"Sampledata_" + now.Hour + "_" + now.Minute + "_" + now.Second + ".csv",
                        CreationCollisionOption.ReplaceExisting));
            var lines = new List<string>();
            for (var index = 0; index < dataChunks.Count; index++)
            {
                lines.Add(dataChunks[index].ToString());
            }
            await FileIO.AppendLinesAsync(outputFile, lines);
            dataChunks.Clear();
            */

            double averageAccX = 0;
            foreach (DataPoint point in points)
            {
                averageAccX += point.Ticks;
            }
            averageAccX /= points.Length;
            averageAccX /= 64848;

            if ((Math.Ceiling(averageAccX) % 1000 == 2) && ((DateTime.Now - this.lastMovement).Seconds > 1))
            {         
                this.lastMovement = DateTime.Now;
                return true;
            }

            return false;
        }

        public DataPoint[] GetCurrentChunk()
        {
            lock (this.dataPoints)
            {
                return this.dataPoints.ToArray();
            }
        }

        public void EnqueueData(DataPoint datapoint)
        {
            this.dataPoints.Enqueue(datapoint);

            // Throw out old data
            if (this.dataPoints.Count > ItemsInChunk)
            {
                DataPoint toChug;
                this.dataPoints.TryDequeue(out toChug);
            }
        }
    }
}
