using System;

namespace DoLess.Bindings
{
    public class BindingTraceEventArgs : EventArgs
    {
        internal BindingTraceEventArgs(BindingTraceEventType type, string message, Exception exception = null) : base()
        {
            this.EventType = type;
            this.Message = $"DoLess.Bindings - {type.ToString()}: {message}.";
            this.Exception = exception;
        }

        public BindingTraceEventType EventType { get; }

        public string Message { get; }

        public Exception Exception { get; }
    }
}
