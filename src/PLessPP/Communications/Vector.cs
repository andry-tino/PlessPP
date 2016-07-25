/// <summary>
/// Vector.cs - 2015
/// </summary>

namespace PLessPP.Communications
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Vector
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "AccX", IsRequired = true)]
        public double AccelerationX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "AccY", IsRequired = true)]
        public double AccelerationY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "AccZ", IsRequired = true)]
        public double AccelerationZ { get; set; }
    }
}
