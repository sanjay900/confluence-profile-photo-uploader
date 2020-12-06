using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VanClinic.Libraries.LogWriter
{
    public class Log
    {
        // Private Variables
        private List<LogEntry> logEntries = new List<LogEntry>();
        private String logPath = String.Empty;

        // Properties
        public List<LogEntry> Entries
        {
            get { return logEntries; }
            set { logEntries = value; }
        }
        public String LogPath
        {
            get { return logPath; }
            set { logPath = value; }
        }


        //Methods
        /// <summary>
        /// This method will write a log file where each item is on one line and the events are grouped with a blank line in between.
        /// event1item1event1value1
        /// event1item2event1value2
        /// 
        /// event2item1event2value1
        /// event2item2event2value2
        /// </summary>
        public void WriteLog()
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;

            #region Create Path and File

            // If the log path and file do not exist create them.
            try
            {

                // Create the log directory if it does not exist.
                if (!Directory.Exists(Path.GetDirectoryName(logPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(logPath));
                }
                else
                {
                    // Create the log file if it does not exist.
                    if (!File.Exists(logPath))
                    {
                        fileStream = File.Create(logPath);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                //Clean up
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }

            #endregion

            #region Write to the Log File

            //Open the log file and append the entries
            try
            {
                fileStream = new FileStream(logPath, FileMode.Append, FileAccess.Write);
                streamWriter = new StreamWriter(fileStream);

                streamWriter.WriteLine(GetLogData());
            }
            catch
            {
            }
            finally
            {
                //Clean up
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }

            #endregion
        }

        /// <summary>
        /// This method will write a log file where the events are on one line each in key value format. (ex. item1="value1" item2="value2" etc...)
        /// </summary>
        public void WriteFlatLog()
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;

            try
            {
                // Create the log directory if it does not exist.
                if (!Directory.Exists(Path.GetDirectoryName(logPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(logPath));
                }

                //Open the log file and append the entries
                fileStream = new FileStream(logPath, FileMode.Append, FileAccess.Write);
                streamWriter = new StreamWriter(fileStream);

                streamWriter.WriteLine(GetFlatLogData());
            }
            catch
            {
            }
            finally
            {
                //Clean up
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }

        public override string ToString()
        {
            return GetLogData();
        }

        private String GetLogData()
        {
            String LogData = String.Empty;
            Int32 i = 0;

            foreach (LogEntry entry in Entries)
            {
                if (i != 0)
                {
                    LogData += "\r\n";
                }
                foreach (LogEntryItem item in entry.Items)
                {
                    LogData += item.ItemName + item.ItemValue + "\r\n";
                }
                i++;
            }

            return LogData;
        }

        private String GetFlatLogData()
        {
            StringBuilder LogData = new StringBuilder();
            Int32 x = 0;
            Int32 i = 0;

            foreach (LogEntry entry in Entries)
            {
                if (x != 0)
                {
                    LogData.Append("\r\n");
                }

                i = 0;
                foreach (LogEntryItem item in entry.Items)
                {
                    if (i != 0)
                    {
                        LogData.Append(" ");
                    }

                    //Add item ... also make sure item value has not double quotes or line breaks.
                    LogData.AppendFormat("{0}=\"{1}\"", item.ItemName.ToLower(), item.ItemValue.Replace("\"", "'").Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", " "));

                    i++;
                }

                x++;
            }

            return LogData.ToString();
        }
    }
}