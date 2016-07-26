/// <summary>
/// Utils.cs
/// </summary>

#undef SILENT_LOG_ERROR

namespace PLessPP.Testing
{
    using System;
    using System.Diagnostics;
    using System.IO;

    using PLessPP.AI;
    using PLessPP.Testing.Testability;

    internal static class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="results"></param>
        /// <param name="path"></param>
        public static void WriteMWMSResults(MultiWindowMultiShiftResults results, string path)
        {
            string results2String = results.ToString();

            try
            {
                Permissions.GrantFolderAccess(Path.GetDirectoryName(path));
                File.WriteAllText(path, results2String);
            }
            catch (Exception e)
            {
#if SILENT_LOG_ERROR
                WriteErrorToEventLog(e.Message, e);
#else
                throw e;
#endif
            }
        }

        private static void WriteErrorToEventLog(string message, Exception exception)
        {
            string source = "PLessPP";
            string log = "Application";
            string logEvent = "Test error - " + message + ". Exception: " + exception.ToString();

            try
            {
                if (!EventLog.SourceExists(source))
                {
                    EventLog.CreateEventSource(source, log);
                }

                EventLog.WriteEntry(source, logEvent, EventLogEntryType.Error);
            }
            catch
            {
                // Nothing to do
            }
        }
    }
}
