/// <summary>
/// Sequence.cs
/// </summary>

namespace PLessPP.Similarity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using SequenceUnit = System.Double;

    /// <summary>
    /// 
    /// </summary>
    public class Sequence : IEnumerable<SequenceUnit>
    {
        private IEnumerable<SequenceUnit> sequence;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sequence"/> class.
        /// </summary>
        /// <param name="values"></param>
        public Sequence(params SequenceUnit[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            this.sequence = new SequenceUnit[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                (this.sequence as SequenceUnit[])[i] = values[i];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SequenceUnit this[int index]
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
        public IEnumerator<SequenceUnit> GetEnumerator()
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
    }
}
