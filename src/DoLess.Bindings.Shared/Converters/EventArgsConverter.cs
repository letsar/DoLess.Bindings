using System;

namespace DoLess.Bindings
{
    internal class EventArgsConverter<TSource, TItem> : IConverterFromSource<TSource, object>
        where TSource : EventArgs<TItem>
        where TItem : class
    {
        public object ConvertFromSource(TSource value)
        {
            return value.Item;
        }
    }
}