using System;

namespace DoLess.Bindings
{
    public static class Bindings
    {
        static Bindings()
        {
        }

        public static event EventHandler<BindingTraceEventArgs> Trace = delegate { };

        internal static void LogError(string message, Exception ex = null)
        {
            Log(BindingTraceEventType.Error, message, ex);
        }

        internal static void LogWarning(string message)
        {
            Log(BindingTraceEventType.Warning, message);
        }

        private static void Log(BindingTraceEventType type, string message, Exception exception = null)
        {
            Trace(null, new BindingTraceEventArgs(type, message, exception));
        }
    }
}
