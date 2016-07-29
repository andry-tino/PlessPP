/**
 * Sequence.cs
 * 
 * PLessPP - Copyright (C) 2016
 * Andrea Tino, Constantin Daniil, Jeroen Rietveld, 
 * Liansheng Hua, Nikola Kukrika, Sam van Lieshout
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

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

            var points = new Point[values.Length];

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

        /// <summary>
        /// Gets a real signal representation.
        /// </summary>
        public IEnumerable<double> Modules
        {
            get { return this.sequence.Select(point => point.Module); }
        }

        /// <summary>
        /// Renders the signal.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var modules = this.Modules;

            const string openBracket = "{";
            const string closeBracket = "}";
            const string separator = ",";

            string output = string.Empty;

            output += openBracket;

            foreach (var number in modules)
            {
                output += openBracket;
                output += number.ToString();
                output += separator;
            }

            output += closeBracket;

            output = output.Replace(string.Format("{0}{1}", separator, closeBracket), closeBracket);

            return output;
        }

        private void CalcStats()
        {
            Point sum = new Point(0, 0, 0, 0, 0, 0, 0);

            foreach (var point in this.sequence)
            {
                sum += point;
            }

            this.Mean = sum / this.Length;

            Point squareSum = new Point(0, 0, 0, 0, 0, 0, 0);

            foreach (var point in this.sequence)
            {
                squareSum += (point - this.Mean) * (point - this.Mean);
            }

            //this.Variance = squareSum / this.Length;
            this.Variance = Point.Sqrt(squareSum / this.Length);
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
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns>
        /// A <see cref="Sequence"/> truncated to the specified slice and with
        /// the same <see cref="INormalizer"/> of the original sequence.
        /// </returns>
        public Sequence this[int index1, int index2]
        {
            get
            {
                if (index1 < 0)
                {
                    throw new ArgumentException("Index cannot be negative!", nameof(index1));
                }
                if (index2 < 0)
                {
                    throw new ArgumentException("Index cannot be negative!", nameof(index2));
                }

                if (index1 > index2)
                {
                    throw new ArgumentException("Invalid range specified!", nameof(index1));
                }

                List<Point> points = new List<Point>();

                for (int i = index1; i <= index2; i++)
                {
                    points.Add(this.sequence.ElementAt(i));
                }

                return new Sequence(this.normalizer, points.ToArray());
            }
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
