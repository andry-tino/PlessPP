/// <summary>
/// IPreprocessor.cs
/// </summary>

namespace PLessPP.Similarity
{
    using System;

    using PLessPP.Similarity.Data;

    /// <summary>
    /// 
    /// </summary>
    public interface IPreprocessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        Sequence Preprocess(Sequence sequence);
    }
}
