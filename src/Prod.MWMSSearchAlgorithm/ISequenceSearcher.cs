/// <summary>
/// ISequenceSearcher.cs
/// </summary>

namespace PLessPP.AI.MWMSSearchAlgorithm
{
    using System;

    using PLessPP.Data;

    /// <summary>
    /// 
    /// </summary>
    public interface ISequenceSearcher
    {
        /// <summary>
        /// Searches a particular sequence inside another one.
        /// </summary>
        /// <param name="sequence">The <see cref="Sequence"/> to search into.</param>
        /// <param name="results"></param>
        void Search(Sequence sequence, out object results);
    }
}
