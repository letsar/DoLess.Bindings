using System;
using System.ComponentModel;

namespace DoLess.Bindings
{
    internal static class WeakEventHandlerExtensions
    {
        public static INotifyPropertyChangedWeakEventHandler AddWeakEventHandler(this INotifyPropertyChanged eventSource, EventHandler<PropertyChangedEventArgs> handler)
        {
            return new INotifyPropertyChangedWeakEventHandler(eventSource, handler);
        }

        //public static WeakEventHandler<TEventSource, TEventArgs> AddWeakEventHandler<TEventSource, TEventHandler, TEventArgs>(this TEventSource eventSource, Action<TEventSource, TEventHandler> addHandler, Action<TEventSource, TEventHandler> removeHandler, EventHandler<TEventArgs> handler)
        //    where TEventSource : class
        //    where TEventArgs : EventArgs
        //{
        //    return new WeakEventHandler<TEventSource, TEventArgs>(eventSource, addHandler, removeHandler, handler);
        //}
    }
}
