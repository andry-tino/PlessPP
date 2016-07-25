/// <summary>
/// Service.cs - 2015
/// </summary>

namespace PLessPP.Communications
{
    using System;
    using System.ServiceModel;

    /// <summary>
    /// 
    /// </summary>
    [ServiceContract]
    public class Service
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        [OperationContract]
        public void Main(string[] args)
        {
        }
    }
}
