/// <summary>
/// Sequence.cs
/// </summary>

namespace PLessPP.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public class Sequence : IEnumerable<Point>
    {
        private IEnumerable<Point> sequence;

        private INormalizer normalizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sequence"/> class.
        /// </summary>
        /// <param name="norm"></param>
        /// <param name="values"></param>
        public Sequence(INormalizer norm, params Point[] values) : this(values)
        {       
            this.normalizer = norm;
            this.CalcStats();
        }

        internal Sequence(Point[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            var points = new List<Point>(values.Length);

            for (int i = 0; i < values.Length; i++)
            {
                points[i] = values[i];
            }

            this.sequence = new ReadOnlyCollection<Point>(points);
        }

        public Sequence Normalize()
        {
            return this.normalizer?.Normalize(this);
        }

        private void CalcStats()
        {
            Point sum = new Point(0, 0, 0, 0, 0, 0, 0);

            foreach (var point in sequence)
            {
                sum += point;
            }
            this.Mean = sum / this.Length;

            Point squareSum = new Point(0, 0, 0, 0, 0, 0, 0);

            foreach (var point in sequence)
            {
                squareSum += (point - this.Mean) * (point - this.Mean);
            }

            this.Variance = squareSum / this.Length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point this[int index]
        {
            get { return this.sequence.ElementAt(index); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Length
        {
            get { return this.sequence.Count(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Point> GetEnumerator()
        {
            return this.sequence.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.sequence.GetEnumerator();
        }

        public Point Mean { get; set; }

        public Point Variance { get; set; }

    }
}
