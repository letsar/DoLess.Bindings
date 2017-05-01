using System;

namespace DoLess.Bindings
{
    public interface IEventBinding<TSource, TTarget, TEventArgs> : 
        IBinding
        where TSource : class
        where TTarget : class
        where TEventArgs : EventArgs
    {
    }
}
