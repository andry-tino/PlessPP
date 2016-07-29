/**
 * ChunkConsumer.cs
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

#undef LOG_SIGNALS

namespace PLessPP.AI.MWMSSearchAlgorithm
{
    using System;
    using System.Threading;

    using PLessPP.Data;

    /// <summary>
    /// 
    /// </summary>
    public delegate void GesturePerformed();

    /// <summary>
    /// This class is responsible for continuosly read the buffer and react to positives.
    /// </summary>
    public class ChunkConsumer : IDisposable
    {
        private readonly ISequenceSearcher searchAlgorithm;
        private readonly ISearchDecider searchDecider;
        private readonly IChunkBuffer chunkBuffer;
        private readonly INormalizer normalizer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchAlgorithm"></param>
        /// <param name="chunkBuffer"></param>
        public ChunkConsumer(ISequenceSearcher searchAlgorithm, ISearchDecider searchDecider, IChunkBuffer chunkBuffer, INormalizer normalizer)
        {
            this.searchAlgorithm = searchAlgorithm;
            this.searchDecider = searchDecider;
            this.chunkBuffer = chunkBuffer;
            this.normalizer = normalizer;
        }

        /// <summary>
        /// 
        /// </summary>
        public event GesturePerformed OnGesturePerformed;

        /// <summary>
        /// 
        /// </summary>
        public void Run()
        {
            for (;;)
            {
                // TODO: Add a thread sleep guard just here

                Point[] chunk = this.chunkBuffer.GetCurrentChunk();

                if (chunk == null || chunk.Length == 0)
                {
                    continue;
                }

                object results;
                this.searchAlgorithm.Search(new Sequence(normalizer, chunk), out results);

#if LOG_SIGNALS
                System.IO.File.WriteAllText(@"C:\temp\plesspp_" + DateTime.Now.Ticks + ".txt", (results as MultiWindowMultiShiftResults).ToString());
#endif

                bool matchFound = this.searchDecider.MatchFound(results);

                if (matchFound)
                {
                    this.OnGesturePerformed?.Invoke();
                    //this.chunkBuffer.ClearBuffer();
                    Thread.Sleep(3000);
                }
            }
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            // Might need here to perform some disposal
        }
    }
}
