/// <summary>
/// SimpleNormalizer.cs
/// </summary>

namespace PLessPP.Data
{
    public class SimpleNormalizer : INormalizer
    {
        public Sequence Normalize(Sequence seq)
        {
            Point[] normalizedPoints = new Point[seq.Length];
            for (int i = 0; i < seq.Length; i++)
            {
                normalizedPoints[i] = (seq[i] - seq.Mean) / seq.Variance;
            }
            return new Sequence(normalizedPoints);
        }
    }
}
