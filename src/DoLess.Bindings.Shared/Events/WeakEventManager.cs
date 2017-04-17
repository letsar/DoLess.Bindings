using System;
using System.Runtime.CompilerServices;
using DoLess.Bindings.Events;
using DoLess.Bindings.Helpers;

namespace DoLess.Bindings
{
    /// <summary>
    /// WeakEventManager base class. Inspired by the ReactiveUI WeakEventManager class.
    /// </summary>
    /// <typeparam name="TEventSource">The type of the event source.</typeparam>    
    /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
    internal class WeakEventManager<TEventSource, TEventArgs>
        where TEventSource : class
        where TEventArgs : EventArgs
    {
        private static readonly Lazy<WeakEventManager<TEventSource, TEventArgs>> Instance =
            new Lazy<WeakEventManager<TEventSource, TEventArgs>>(() => new WeakEventManager<TEventSource, TEventArgs>());

        private static readonly object StaticSource;

        /// <summary>
        /// Mapping from the source of the event to the list of handlers. This is a CWT to ensure it does not leak the source of the event.
        /// </summary>
        private readonly ConditionalWeakTable<object, WeakEventHandlerList<TEventSource, TEventArgs>> sourceToWeakHandlers;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakEventManager{TEventSource, TEventHandler, TEventArgs}"/> class.
        /// Protected to disallow instances of this class and force a subclass.
        /// </summary>
        protected WeakEventManager()
        {
            this.sourceToWeakHandlers = new ConditionalWeakTable<object, WeakEventHandlerList<TEventSource, TEventArgs>>();
        }

        /// <summary>
        /// Gets the current value of this WeakEventManager.
        /// </summary>
        public static WeakEventManager<TEventSource, TEventArgs> Current => Instance.Value;

        public void AddHandler(TEventSource source, EventHandler<TEventArgs> handler)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(handler, nameof(handler));

            this.AddWeakHandler(source, handler);
        }

        public void RemoveHandler(TEventSource source, EventHandler<TEventArgs> handler)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(handler, nameof(handler));

            this.RemoveWeakHandler(source, handler);
        }

        public void DeliverEvent(TEventSource source, TEventArgs args)
        {
            bool hasStaleEntries = false;
            var weakEventHandlerList = this.GetWeakEventHandlerList(source);

            if (weakEventHandlerList != null)
            {
                hasStaleEntries = weakEventHandlerList.DeliverEvent(source, args);
            }

            if (hasStaleEntries)
            {
                this.Purge(source);
            }
        }

        private void AddWeakHandler(TEventSource source, EventHandler<TEventArgs> handler)
        {
            var weakEventHandlerList = this.GetWeakEventHandlerList(source);
            if (weakEventHandlerList == null)
            {
                weakEventHandlerList = new WeakEventHandlerList<TEventSource, TEventArgs>();
                this.sourceToWeakHandlers.Add(source, weakEventHandlerList);
            }
            weakEventHandlerList.Add(source, handler);
            this.Purge(source);
        }

        private void RemoveWeakHandler(TEventSource source, EventHandler<TEventArgs> handler)
        {
            var weakEventHandlerList = this.GetWeakEventHandlerList(source);
            if (weakEventHandlerList != null)
            {
                if (weakEventHandlerList.Remove(source, handler) && weakEventHandlerList.Count == 0)
                {
                    this.sourceToWeakHandlers.Remove(source);
                }
            }
        }

        private void Purge(TEventSource source)
        {
            this.GetWeakEventHandlerList(source, true);
        }

        private bool CloneIfDelivering(object source, ref WeakEventHandlerList<TEventSource, TEventArgs> weakEventHandlerList)
        {
            var isDelivering = weakEventHandlerList.IsDelivering;
            if (isDelivering)
            {
                weakEventHandlerList = weakEventHandlerList.Clone();
                this.sourceToWeakHandlers.Remove(source);
                this.sourceToWeakHandlers.Add(source, weakEventHandlerList);
            }

            return isDelivering;
        }

        private WeakEventHandlerList<TEventSource, TEventArgs> GetWeakEventHandlerList(TEventSource source, bool purgeIfNotDelivering = false)
        {
            WeakEventHandlerList<TEventSource, TEventArgs> weakEventHandlerList = null;

            // If the source is null, the CWT needs a key, so we use the StaticSource for this.
            object key = source ?? StaticSource;

            if (this.sourceToWeakHandlers.TryGetValue(key, out weakEventHandlerList))
            {
                if (!this.CloneIfDelivering(key, ref weakEventHandlerList) && purgeIfNotDelivering)
                {
                    weakEventHandlerList.Purge();
                }
            }

            return weakEventHandlerList;
        }
    }
}
