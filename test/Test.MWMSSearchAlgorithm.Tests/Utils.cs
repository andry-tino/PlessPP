/// <summary>
/// Utils.cs
/// </summary>

#undef SILENT_LOG_ERROR

namespace PLessPP.Testing.MWMSSearchAlgorithm
{
    using System;
    using System.Diagnostics;
    using System.IO;

    using PLessPP.AI.MWMSSearchAlgorithm;
    using PLessPP.Data;
    using PLessPP.Testing.Testability;

    internal static class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardinalLength"></param>
        /// <returns></returns>
        public static int[] GenerateWindowTriple(int cardinalLength, int maxLength)
        {
            return new int[] 
            {
                cardinalLength,
                (int)Math.Ceiling(cardinalLength / 2d),
                (int)Math.Floor((cardinalLength + maxLength) / 2d)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="results"></param>
        /// <param name="path"></param>
        public static void WriteMWMSResults(MultiWindowMultiShiftResults results, string path)
        {
            WriteLog(results.ToString(), path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="path"></param>
        public static void WriteSequence(Sequence sequence, string path)
        {
            WriteLog(sequence.ToString(), path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        /// <param name="path"></param>
        public static void WriteTimingRecord(TimingRecord record, string path)
        {
            WriteLog(record.ToString(), path);
        }

        private static void WriteLog(string content, string path)
        {
            try
            {
                Permissions.GrantFolderAccess(Path.GetDirectoryName(path));
                File.WriteAllText(path, content);
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

        #region Types

        /// <summary>
        /// 
        /// </summary>
        public struct TimingRecord
        {
            /// <summary>
            /// Start time in ticks.
            /// </summary>
            public long Start { get; private set; }

            /// <summary>
            /// Stop time in ticks.
            /// </summary>
            public long Stop { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public static TimingRecord Create()
            {
                return new TimingRecord() { Start = DateTime.Now.Ticks, Stop = -1 };
            }

            /// <summary>
            /// Marks the record.
            /// </summary>
            public void StopWatch()
            {
                if (this.Stop != -1)
                {
                    throw new InvalidOperationException("Stopiing has already been called! Cannot call it twice!");
                }

                this.Stop = DateTime.Now.Ticks;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return string.Format("Ticks: {0}, Milliseconds: {1}", this.ElapsedTime, 
                    new TimeSpan(this.ElapsedTime).TotalMilliseconds);
            }

            /// <summary>
            /// Gets the elapsed time in ticks.
            /// </summary>
            public long ElapsedTime
            {
                get
                {
                    if (this.Stop == -1)
                    {
                        throw new InvalidOperationException("Need to stopwatch the record before performing this operation!");
                    }

                    return this.Stop - this.Start;
                }
            }
        }

        #endregion
    }
}
