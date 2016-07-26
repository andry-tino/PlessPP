/// <summary>
/// ISequenceSearcher.cs
/// </summary>

namespace PLessPP.AI
{
    using System;

    using PLessPP.Data;
    using PLessPP.Similarity.Data;

    /// <summary>
    /// 
    /// </summary>
    public interface ISequenceSearcher
    {
        /// <summary>
        /// Searches a particular sequence inside another one.
        /// </summary>
        /// <param name="sequence">The <see cref="Sequence"/> to search into.</param>
        /// <param name="result"></param>
        void Search(Sequence sequence, out int results); // not int, we need to create a class for this
    }
}
