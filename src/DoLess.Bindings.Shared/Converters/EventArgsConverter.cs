using System;

namespace DoLess.Bindings
{
    internal class EventArgsConverter<TSource, TItemProperty> : IConverterFromSource<TSource, object>
        where TSource : EventArgs<TItemProperty>
        where TItemProperty : class
    {
        public object ConvertFromSource(TSource value)
        {
            return value.Item;
        }
    }
}