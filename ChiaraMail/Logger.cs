using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Reflection;


namespace ChiaraMail
{
    internal class Logger
    {
        private static readonly object LogLock = new object();
        //Logging

        /// <summary>
        /// Write separator line to log
        /// </summary>
        public static void Init()
        {
            //write separator directly to log file        
            string logPath = GetCurrentLogPath();
            if (logPath.Length > 0)
            {
                var logFile = new StreamWriter(logPath, true);
                try
                {
                    //append message to trace log
                    logFile.WriteLine(Environment.NewLine +
                        new string((char)(61), 15));
                }
                finally
                {
                    logFile.Close();
                }
            }
        }

        /// <summary>
        /// Log error message
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="message">Text to write</param>
        public static void Error(string proc, string message)
        {
            if (GetTraceLevel() > TraceLevel.Off)
            {
                string entry = proc + " (" + message + ")";
                LogToFile(entry);
            }
        }

        /// <summary>
        /// Log warning message
        /// </summary>
        /// <param name="proc">Calling procedure</param>
        /// <param name="message">Text to write</param>
        public static void Warning(string proc, string message)
        {
            if (GetTraceLevel() >= TraceLevel.Warning)
            {
                string entry = proc + " (" + message + ")";
                LogToFile(entry);
            }
        }

        /// <summary>
        /// Log information message
        /// </summary>
        /// <param name="proc">Calling procedure</param>
        /// <param name="message">Text to write</param>
        public static void Info(string proc, string message)
        {
            if (GetTraceLevel() >= TraceLevel.Info)
            {
                string entry = proc + " (" + message + ")";
                LogToFile(entry);
            }
        }

        /// <summary>
        /// Log verbose message
        /// </summary>
        /// <param name="proc">Calling procedure</param>
        /// <param name="message">Text to write</param>
        public static void Verbose(string proc, string message)
        {
            //this level only gets written to text file
            if (GetTraceLevel() >= TraceLevel.Verbose)
            {
                string entry = proc + " (" + message + ")";
                LogToFile(entry);
            }
        }

        public static string LogsPath
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "ChiaraMail\\Logs\\");
                //var appData = Environment.GetEnvironmentVariable("AppData");
                //return Path.Combine(appData, "ChiaraMail\\Logs\\");
            }
        }

        /// <summary>
        /// If current app logging level >= logLevel then write specified message to event and text logs
        /// </summary>
        /// <param name="message"></param>
        private static void LogToFile(string message)
        {
            string entryPath = GetCurrentLogPath();
            if (entryPath.Length <= 0) return;
            lock (LogLock)
            {
                using (var logFile = new StreamWriter(entryPath, true))
                {
                    //append message to trace log
                    logFile.WriteLine("{0}:{1}", DateTime.Now, message);
                    logFile.Flush();
                }
            }
        }

        /// <summary>
        /// Get/create log path for current Day of Week.
        /// </summary>
        /// <returns>Path to log file</returns>
        /// <remarks>
        /// </remarks>
        private static string GetCurrentLogPath()
        {
            //check for today's folder
            try
            {
                string folderPath = Path.Combine(LogsPath, DateTime.Today.ToString("ddd"));
                var di = new DirectoryInfo(folderPath);
                // Create the directory only if it does not already exist.
                if (di.Exists == false)
                {
                    di.Create();
                }
                string entryPath = Path.Combine(folderPath, "OutlookECS.log");
                if (File.Exists(entryPath))
                {
                    lock (LogLock)
                    {
                        //if last write is not today then delete the file so we can start fresh
                        if (File.GetLastWriteTime(entryPath).Day != DateTime.Today.Day)
                        {
                            File.Delete(entryPath);
                        }
                        else
                        {
                            //truncate existing file if it exceeds limit
                            TruncateFile(entryPath);
                        }
                    }
                }
                return entryPath;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Truncate file that exceeds LOG_MAX by removing lines from beginning
        /// </summary>
        /// <param name="filePath">Path to file that will be truncated</param>
        /// <remarks>
        /// </remarks>
        private static void TruncateFile(string filePath)
        {
            int logMax = Convert.ToInt32(2 * (Math.Pow(1024, 2)));
            //measure amount that existing file exceeds limit
            var f = new FileInfo(filePath);
            long lngExcess = f.Length - logMax;
            if (lngExcess <= 0) return;
            string tempName = Path.GetTempFileName();

            using (var reader = new StreamReader(filePath))
            {
                var sbDump = new StringBuilder();
                //strip off lines that exceed limit
                while (sbDump.Length < lngExcess)
                {
                    sbDump.Append(reader.ReadLine());
                }
                //write the rest to the temp file
                using (var writer = new StreamWriter(tempName))
                {
                    while (reader.Peek() != -1)
                    {
                        writer.WriteLine(reader.ReadLine());
                    }
                }
            }
            File.Delete(filePath);
            //replace existing file with temp copy
            File.Move(tempName, filePath);
        }

        /// <summary>
        /// Evaluate current trace level
        /// </summary>
        /// <returns>TraceLevel</returns>
        /// <remarks></remarks>
        internal static TraceLevel GetTraceLevel()
        {
            string traceLevel;
            //read from settings file - current user value takes precedence
            try
            {
                traceLevel = Properties.Settings.Default.LogLevel;
            }
            catch
            {
                traceLevel = "information";
            }
            switch (traceLevel.ToLower())
            {
                case "off":
                case "none":
                case "":
                    return TraceLevel.Off;
                case "error":
                    return TraceLevel.Error;
                case "warning":
                    return TraceLevel.Warning;
                case "information":
                    return TraceLevel.Info;
                case "verbose":
                    return TraceLevel.Verbose;
                default:
                    return TraceLevel.Info;
            }
        }

        public static void ListAssemblies(string source)
        {
            try
            {
                if (GetTraceLevel() != TraceLevel.Verbose)
                {
                    return;
                }
                AppDomain currentDomain = AppDomain.CurrentDomain;
                //Make an array for the list of assemblies.
                Assembly[] assems = currentDomain.GetAssemblies();

                //List the assemblies in the current application domain.
                var sb = new StringBuilder();
                sb.Append("Loaded assemblies in " + currentDomain.FriendlyName + ":" + Environment.NewLine);
                foreach (Assembly assem in assems)
                {
                    sb.Append(string.Empty.PadLeft(4) + assem + Environment.NewLine);
                }
                Info(source, sb.ToString());
            }
            catch (Exception ex)
            {
                Error("ListAssemblies", ex.ToString());
            }
        }
    }
} //end of root namespace