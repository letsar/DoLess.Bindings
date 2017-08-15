using System;

namespace DoLess.Bindings
{
    public class ItemEventArgs<T> : EventArgs
    {
        public ItemEventArgs(T item)
        {
            this.Item = item;
        }

        public T Item { get; }
    }
}
