/// <summary>
/// Sequence.cs
/// </summary>

namespace PLessPP.Similarity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using SrequenceUnit = System.Double;

    /// <summary>
    /// 
    /// </summary>
    public class Sequence : IEnumerable<SrequenceUnit>
    {
        private IEnumerable<SrequenceUnit> sequence;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sequence"/> class.
        /// </summary>
        public Sequence()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public SrequenceUnit this[int index]
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
        public IEnumerator<SrequenceUnit> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
