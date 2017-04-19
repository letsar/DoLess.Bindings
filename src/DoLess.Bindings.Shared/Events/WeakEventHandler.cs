using System;
using System.ComponentModel;
using DoLess.Bindings.Extensions;

namespace DoLess.Bindings.Events
{
    internal class WeakEventHandler<TEventSource, TEventArgs>
        where TEventSource : class
        where TEventArgs : EventArgs
    {
        private readonly WeakReference<TEventSource> weakSource;
        private readonly WeakReference<EventHandler<TEventArgs>> weakHandler;

        public WeakEventHandler(TEventSource source, EventHandler<TEventArgs> handler)
        {
            this.weakSource = new WeakReference<TEventSource>(source);
            this.weakHandler = new WeakReference<EventHandler<TEventArgs>>(handler);
        }

        public EventHandler<TEventArgs> Handler => this.weakHandler.GetOrDefault();

        public bool IsActive => this.weakSource.IsAlive() && this.weakHandler.IsAlive();

        public bool Match(TEventSource source, EventHandler<TEventArgs> handler)
        {
            TEventSource originalSource = this.weakSource.GetOrDefault();
            EventHandler<TEventArgs> originalHandler = this.weakHandler.GetOrDefault();

            return ReferenceEquals(originalSource, source) &&
                       (ReferenceEquals(originalHandler, handler) ||
                        (originalHandler?.GetType() == handler?.GetType() && 
                        Equals(originalHandler?.Target, handler?.Target)));
        }
    }
}
