/// <summary>
/// IDataConnector.cs
/// </summary>

using PLessPP.Data;

namespace PLessPP.Testing.Testability.Data
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public interface IDataConnector
    {
        /// <summary>
        /// 
        /// </summary>
        Sequence Data { get; }
    }
}
