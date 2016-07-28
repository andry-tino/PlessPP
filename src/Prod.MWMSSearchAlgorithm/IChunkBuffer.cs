// --------------------------------------------------------------------------
// <copyright file="IChunkBuffer.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------

namespace PLessPP.AI.MWMSSearchAlgorithm
{
    using PLessPP.Data;

    /// <summary>
    ///     The interface for the chunk buffer.
    /// </summary>
    public interface IChunkBuffer
    {
        Point[] GetCurrentChunk();

        void ClearBuffer();
    }
}