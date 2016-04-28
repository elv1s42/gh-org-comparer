using System;
using System.IO;
using System.Reflection;

namespace OrgComparer
{
    public static class Log
    {
        private const string LogFile = @"Log.txt";

        public static string Output
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return path;
            }
        }
        
        private static void WriteToFile(string msg, string fileName)
        {
            using (var sw = File.AppendText(Path.Combine(Output, fileName)))
            {
                try
                {
                    var logLine = $"{DateTime.Now:G}: {msg}";
                    sw.WriteLine(logLine);
                }
                finally
                {
                    sw.Close();
                }
            }
        }
        
        public static void Write(string msg)
        {
            using (var sw = File.AppendText(Path.Combine(Output, LogFile)))
            {
                try
                {
                    var logLine = $"{DateTime.Now:G}: {msg}";
                    sw.WriteLine(logLine);
                }
                catch (Exception ex)
                {
                    Exception(ex);
                }
                finally
                {
                    sw.Close();
                }
            }
        }

        public static void Exception(Exception exception, string exceptionMessage = "")
        {
            var msg = (exceptionMessage.Equals("") ? "Exception!" : exceptionMessage) + Environment.NewLine
                + " Message: " + Environment.NewLine + exception.Message + Environment.NewLine +
                "StackTrace: " + Environment.NewLine + exception.StackTrace;
            var inner = exception.InnerException;
            while (inner != null)
            {
                msg = msg + Environment.NewLine + " Inner Exception: " + Environment.NewLine +
                    inner.Message + Environment.NewLine +
                "StackTrace: " + Environment.NewLine + inner.StackTrace;
                inner = inner.InnerException;
            }
            WriteToFile(msg, "Exception_" + DateTime.Now.ToString("ddMMyyHHmmssfff") + ".txt");
        }

        public static void Warning(string warningMessage)
        {
            WriteToFile(warningMessage, "Warning_" + DateTime.Now.ToString("ddMMyyHHmmssfff") + ".txt");
        }
    }
}
