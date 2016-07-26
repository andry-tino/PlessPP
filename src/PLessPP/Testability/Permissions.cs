/// <summary>
/// Permissions.cs
/// </summary>

namespace PLessPP.Testing.Testability
{
    using System;
    using System.IO;

    using System.Security.AccessControl;

    /// <summary>
    /// 
    /// </summary>
    public static class Permissions
    {
        public static void GrantFolderAccess(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            DirectorySecurity ds = di.GetAccessControl();

            string account = Accounts.CurrentAccount;

            ds.AddAccessRule(new FileSystemAccessRule(account, FileSystemRights.WriteData, AccessControlType.Allow));
            ds.AddAccessRule(new FileSystemAccessRule(account, FileSystemRights.Write, AccessControlType.Allow));
            ds.AddAccessRule(new FileSystemAccessRule(account, FileSystemRights.ReadData, AccessControlType.Allow));
            ds.AddAccessRule(new FileSystemAccessRule(account, FileSystemRights.Read, AccessControlType.Allow));

            di.SetAccessControl(ds);
        }
    }
}
