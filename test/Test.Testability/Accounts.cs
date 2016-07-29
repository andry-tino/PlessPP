/**
 * Accounts.cs
 * 
 * Andrea Tino, Constantin Daniil, Jeroen Rietveld, 
 * Liansheng Hua, Nikola Kukrika, Sam van Lieshout
 */

namespace PLessPP.Testing.Testability
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public static class Accounts
    {
        /// <summary>
        /// 
        /// </summary>
        public static string CurrentAccount
        {
            get { return System.Security.Principal.WindowsIdentity.GetCurrent().Name; }
        }
    }
}
