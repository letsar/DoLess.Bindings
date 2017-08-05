using System;
using System.Collections.Generic;
using System.Linq;

namespace DoLess.Bindings
{
    public static class Bindings
    {
        static Bindings()
        {           
        }

        public static event EventHandler<BindingTraceEventArgs> Trace = delegate { };

        internal static void LogError(string message)
        {
            Log(BindingTraceEventType.Error, message);
        }

        internal static void LogWarning(string message)
        {
            Log(BindingTraceEventType.Warning, message);
        }

        private static void Log(BindingTraceEventType type, string message)
        {
            Trace(null, new BindingTraceEventArgs(type, message));
        }
    }
}
