/// <summary>
/// ISimilarityAlgorithm.cs
/// </summary>

namespace PLessPP.Similarity
{
    using System;

    using PLessPP.Similarity.Data;

    /// <summary>
    /// 
    /// </summary>
    public interface ISimilarityAlgorithm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence1"></param>
        /// <param name="sequence2"></param>
        /// <returns></returns>
        double ComputeSimilarity(Sequence sequence1, Sequence sequence2);
    }
}
