/// <summary>
/// ChunkConsumer.cs
/// </summary>

namespace PLessPP.AI
{
    using System;

    using PLessPP.Data;
    using PLessPP.Similarity.Data;

    /// <summary>
    /// This class is responsible for continuosly read the buffer and react to positives.
    /// </summary>
    public class ChunkConsumer
    {
        private readonly ISequenceSearcher searchAlgorithm;
        private readonly ISearchDecider searchDecider;
        private readonly IChunkBuffer chunkBuffer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchAlgorithm"></param>
        /// <param name="chunkBuffer"></param>
        public ChunkConsumer(ISequenceSearcher searchAlgorithm, ISearchDecider searchDecider, IChunkBuffer chunkBuffer)
        {
            this.searchAlgorithm = searchAlgorithm;
            this.searchDecider = searchDecider;
            this.chunkBuffer = chunkBuffer;
        }

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
                this.searchAlgorithm.Search(new Sequence(chunk), out results);

                bool matchFound = this.searchDecider.MatchFound(results);

                if (matchFound)
                {
                    // TODO: Activate the logic to react to a match being found
                }
            }
        }
    }
}
