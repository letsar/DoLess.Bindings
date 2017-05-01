using System;

namespace DoLess.Bindings
{
    public interface IEventBinding<TSource, TTarget, TEventArgs>
        where TSource : class
        where TTarget : class
        where TEventArgs : EventArgs
    {
    }
}
