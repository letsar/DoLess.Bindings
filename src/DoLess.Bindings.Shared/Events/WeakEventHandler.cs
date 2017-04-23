using System;
using System.Linq.Expressions;
using System.Reflection;
using DoLess.Bindings.Helpers;

namespace DoLess.Bindings
{
    /// <summary>
    /// A lightweight proxy instance that will subscribe to a given event with a weak reference to the subscribed target.
    /// If the subscriber is garbage collected, then only this WeakEventHandler will remain subscribed and keeped 
    /// in memory instead of the actual subscriber.
    /// This could be considered as an acceptable solution in most cases.
    /// </summary>
    /// <remarks>
    /// Inspired by the work of Aloïs Deniel: https://github.com/aloisdeniel/Wires/blob/master/Sources/Wires/Events/WeakEventHandler.cs
    /// </remarks>
    internal abstract class WeakEventHandler<TEventSource, TEventArgs>
        where TEventSource : class
        where TEventArgs : EventArgs
    {
        private readonly Action<object, object, TEventArgs> proxyEventHandler;
        private readonly object unsubscribeLock = new object();
        private readonly WeakReference<TEventSource> weakEventSource;
        private readonly WeakReference<object> weakEventTarget;
        private bool hasBeenUnsubscribed;

        public WeakEventHandler(TEventSource eventSource, EventHandler<TEventArgs> handler)
        {
            Check.NotNull(eventSource, nameof(eventSource));
            Check.NotNull(handler, nameof(handler));

            this.hasBeenUnsubscribed = false;

            this.weakEventSource = new WeakReference<TEventSource>(eventSource);
            this.weakEventTarget = new WeakReference<object>(handler.Target);

            // Build an independent expression in order to loose all strong reference to handler.Target.
            this.proxyEventHandler = BuildHandlerExpression(handler.GetMethodInfo());

            this.StartListening(eventSource);
        }

        public void Unsubscribe()
        {
            lock (this.unsubscribeLock)
            {
                if (!this.hasBeenUnsubscribed)
                {
                    var eventSource = this.weakEventSource.GetOrDefault();
                    if (eventSource != null)
                    {
                        this.StopListening(eventSource);
                    }
                    this.hasBeenUnsubscribed = true;
                }
            }
        }

        protected void OnEvent(object sender, TEventArgs args)
        {
            var target = this.weakEventTarget.GetOrDefault();
            if (target != null)
            {
                this.proxyEventHandler(target, sender, args);
            }
            else
            {
                // The target no longer lives. We must unsubscribe.
                this.Unsubscribe();
            }
        }

        protected abstract void StartListening(TEventSource source);

        protected abstract void StopListening(TEventSource source);

        private static Action<object, object, TEventArgs> BuildHandlerExpression(MethodInfo methodInfo)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var sender = Expression.Parameter(typeof(object), "sender");
            var args = Expression.Parameter(typeof(TEventArgs), "args");

            var call = Expression.Call(Expression.Convert(instance, methodInfo.DeclaringType), methodInfo, sender, args);
            var expr = Expression.Lambda<Action<object, object, TEventArgs>>(call, instance, sender, args);

            return expr.Compile();
        }
    }
}
