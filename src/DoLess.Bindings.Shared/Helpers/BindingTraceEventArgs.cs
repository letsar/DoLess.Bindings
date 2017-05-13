using System;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    public class BindingTraceEventArgs : EventArgs
    {
        internal BindingTraceEventArgs(BindingTraceEventType type, string message) : base()
        {
            this.EventType = type;
            this.Message = $"DoLess.Bindings - {type.ToString()}: {message}.";
        }

        public BindingTraceEventType EventType { get; }

        public string Message { get; }
    }
}
