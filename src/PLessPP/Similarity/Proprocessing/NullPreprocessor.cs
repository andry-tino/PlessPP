/// <summary>
/// NullPreprocessor.cs
/// </summary>

namespace PLessPP.Similarity
{
    using System;

    using PLessPP.Similarity.Data;

    /// <summary>
    /// 
    /// </summary>
    public class NullPreprocessor : IPreprocessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public Sequence Preprocess(Sequence sequence)
        {
            return sequence;
        }
    }
}
