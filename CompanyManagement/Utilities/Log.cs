using System;
using System.Diagnostics;

namespace CompanyManagement.Utilities
{
    public class Log
    {
        private static Log instance;
        private TraceSource traceSource;

        public static Log Ins
        {
            get
            {
                if (instance == null)
                {
                    instance = new Log();
                }
                return instance;
            }
        }

        private Log()
        {
            traceSource = new TraceSource("Custom Log");
            traceSource.Switch.Level = SourceLevels.Information;
            traceSource.Listeners.Add(new TextWriterTraceListener("CustomLogApp.txt"));
        }

        public void Information(string tag, string message)
        {
            traceSource.TraceInformation($"[TAG='{tag}', Time='{DateTime.Now}', MESSAGE='{message}']");
        }

        public void Error(string tag, string message)
        {
            traceSource.TraceEvent(TraceEventType.Error, 0, $"[TAG='{tag}', Time='{DateTime.Now}', MESSAGE='{message}']");
        }
    }
}
