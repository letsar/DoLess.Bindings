using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DoLess.Bindings.Tests.Events
{
    [TestFixture]
    public class WeakEventTests
    {
        public class EventSource
        {
            public event EventHandler<EventArgs> Event;

            public void Raise()
            {
                this.Event?.Invoke(this, EventArgs.Empty);
            }
        }

        public class WeakReference<TInstance, TSource, TEventArgs> where TInstance : class
        {
            public WeakReference Reference { get; protected set; }
            public Action<TInstance, TSource, TEventArgs> EventAction { get; set; }
            public Action<TInstance, WeakReference<TInstance, TSource, TEventArgs>> DetachAction { get; set; }
            public WeakReference(TInstance instance) { Reference = new WeakReference(instance); }

            public virtual void Handler(object source, TEventArgs eventArgs)
            {
                try
                {
                    EventAction(Reference?.Target as TInstance, (TSource)source, eventArgs);
                }
                catch
                {
                    DetachAction?.Invoke(Reference?.Target as TInstance, this);
                    DetachAction = null;
                }
            }

            ~WeakReference()
            {
                Console.WriteLine("Finalized");
            }
        }

        [Test]
        public void ShouldBeGarbageCollected()
        {
            Console.WriteLine("==");
            EventSource source = new EventSource();

            var listener = new WeakReference<WeakEventTests, EventSource, EventArgs>(this)
            {
                EventAction = (i, s, e) => Console.WriteLine("Received"),
                DetachAction = (i, w) => source.Event -= w.Handler
            };

            source.Event += listener.Handler;

            source.Raise();

            Console.WriteLine("Settin listener to null");

            listener = null;

            TriggerGC();

            source.Raise();

            Console.WriteLine("Setting source to null");

            source = null;

            TriggerGC();
        }

        static void TriggerGC()
        {
            Console.WriteLine("Starting GC");

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Console.WriteLine("GC finished");
        }
    }
}
