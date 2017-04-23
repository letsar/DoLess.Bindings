using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DoLess.Bindings.Helpers;

namespace DoLess.Bindings.EventsOld
{
    /// <summary>
    /// WeakEventManager base class. Inspired by the ReactiveUI WeakEventManager class.
    /// </summary>
    /// <typeparam name="TEventSource">The type of the event source.</typeparam>    
    /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
    public class WeakEventManager<TEventSource, TEventArgs>
        where TEventSource : class
        where TEventArgs : EventArgs
    {
        private static readonly Lazy<WeakEventManager<TEventSource, TEventArgs>> Instance =
            new Lazy<WeakEventManager<TEventSource, TEventArgs>>(() => new WeakEventManager<TEventSource, TEventArgs>());

        private static readonly object StaticSource = new object();

        /// <summary>
        /// Mapping between the target of the delegate (for example a Button) and the handler (EventHandler).
        /// Windows Phone needs this, otherwise the event handler gets garbage collected.
        /// </summary>
        ConditionalWeakTable<object, List<Delegate>> targetToEventHandler = new ConditionalWeakTable<object, List<Delegate>>();

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
            this.AddTargetHandler(handler);
        }

        public void RemoveHandler(TEventSource source, EventHandler<TEventArgs> handler)
        {
            Check.NotNull(source, nameof(source));
            Check.NotNull(handler, nameof(handler));

            this.RemoveWeakHandler(source, handler);
            this.RemoveTargetHandler(handler);
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

        protected void DeliverEventFromObject(object source, TEventArgs args)
        {
            this.DeliverEvent((TEventSource)source, args);
        }

        /// <summary>
        /// Override this method to attach to an event.
        /// </summary>
        /// <param name="source">The source.</param>
        protected virtual void StartListening(TEventSource source)
        {
        }

        /// <summary>
        /// Override this method to detach from an event.
        /// </summary>
        /// <param name="source">The source.</param>
        protected virtual void StopListening(TEventSource source)
        {
        }

        private void AddWeakHandler(TEventSource source, EventHandler<TEventArgs> handler)
        {
            var weakEventHandlerList = this.GetWeakEventHandlerList(source);
            if (weakEventHandlerList == null)
            {
                weakEventHandlerList = new WeakEventHandlerList<TEventSource, TEventArgs>();
                this.sourceToWeakHandlers.Add(source, weakEventHandlerList);
                this.StartListening(source);
            }
            weakEventHandlerList.Add(source, handler);
            this.Purge(source);
        }

        private void AddTargetHandler(EventHandler<TEventArgs> handler)
        {
            object key = handler.Target ?? StaticSource;
            List<Delegate> delegates;

            if (!this.targetToEventHandler.TryGetValue(key, out delegates))
            {
                delegates = new List<Delegate>();
                this.targetToEventHandler.Add(key, delegates);
            }
            delegates.Add(handler);
        }

        private void RemoveWeakHandler(TEventSource source, EventHandler<TEventArgs> handler)
        {
            var weakEventHandlerList = this.GetWeakEventHandlerList(source);
            if (weakEventHandlerList != null)
            {
                if (weakEventHandlerList.Remove(source, handler) && weakEventHandlerList.Count == 0)
                {
                    this.sourceToWeakHandlers.Remove(source);
                    this.StopListening(source);
                }
            }
        }

        private void RemoveTargetHandler(EventHandler<TEventArgs> handler)
        {
            object key = handler.Target ?? StaticSource;
            List<Delegate> delegates;

            if (this.targetToEventHandler.TryGetValue(key, out delegates))
            {
                delegates.Remove(handler);

                if (delegates.Count == 0)
                {
                    this.targetToEventHandler.Remove(key);
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

            // If the source is null (static handlers), the CWT needs a key, so we use the StaticSource for this.
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
