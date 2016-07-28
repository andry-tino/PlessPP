/// <summary>
/// ChunkConsumer.cs
/// </summary>

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
