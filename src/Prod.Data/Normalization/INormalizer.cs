/// <summary>
/// INormalizer.cs
/// </summary>

namespace PLessPP.Data
{
    public interface INormalizer
    {
        /// <summary>
        /// Normalize the raw data
        /// </summary>
        Sequence Normalize(Sequence seq);
    }
}
