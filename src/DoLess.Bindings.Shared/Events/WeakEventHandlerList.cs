using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace DoLess.Bindings.EventsOld
{
    internal class WeakEventHandlerList<TEventSource, TEventArgs>
        where TEventSource : class
        where TEventArgs : EventArgs
    {
        private readonly List<WeakEventHandler<TEventSource, TEventArgs>> handlers;
        private int deliveries = 0;


        public WeakEventHandlerList()
        {
            this.handlers = new List<WeakEventHandler<TEventSource, TEventArgs>>();
        }

        public int Count => this.handlers.Count;

        public bool IsDelivering => this.deliveries > 0;

        public void Add(TEventSource source, EventHandler<TEventArgs> handler)
        {
            this.handlers.Add(new WeakEventHandler<TEventSource, TEventArgs>(source, handler));
        }

        public bool Remove(TEventSource source, EventHandler<TEventArgs> handler)
        {
            var firstMatch = this.handlers.FirstOrDefault(x => x.Match(source, handler));
            if (firstMatch != null)
            {
                return this.handlers.Remove(firstMatch);
            }

            return false;
        }

        public WeakEventHandlerList<TEventSource, TEventArgs> Clone()
        {
            var newList = new WeakEventHandlerList<TEventSource, TEventArgs>();
            newList.handlers.AddRange(this.handlers.Where(x => x.IsActive));
            return newList;
        }

        public bool DeliverEvent(object sender, TEventArgs args)
        {
            bool hasStaleEntries = false;
            Interlocked.Increment(ref this.deliveries);
            try
            {
                for (int i = 0; i < this.Count; i++)
                {
                    var handler = this.handlers[i];
                    if (handler.IsActive)
                    {
                        handler.Handler?.Invoke(sender, args);
                    }
                    else
                    {
                        hasStaleEntries = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Interlocked.Decrement(ref this.deliveries);
            }

            return hasStaleEntries;
        }

        public void Purge()
        {
            for (int i = handlers.Count - 1; i >= 0; i--)
            {
                if (!handlers[i].IsActive)
                {
                    handlers.RemoveAt(i);
                }
            }
        }
    }
}
