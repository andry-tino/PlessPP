/// <summary>
/// ISearchDecider.cs
/// </summary>

namespace PLessPP.AI.MWMSSearchAlgorithm
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface ISearchDecider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        bool MatchFound(object results);
    }
}
