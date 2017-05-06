using System;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T item)
        {
            this.Item = item;
        }

        public T Item { get; }
    }
}
