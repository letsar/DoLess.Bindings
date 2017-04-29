using System;
using System.Reflection;

namespace DoLess.Bindings
{
    internal sealed class DynamicWeakEventHandler<TEventSource, TEventArgs> : WeakEventHandler<TEventSource, TEventArgs>
        where TEventSource : class
        where TEventArgs : EventArgs
    {
        private static readonly MethodInfo OnEventMethodInfo;
        private Delegate eventHandler;
        private EventInfo eventInfo;

        static DynamicWeakEventHandler()
        {
            OnEventMethodInfo = typeof(WeakEventHandler<TEventSource, TEventArgs>).GetMethod(nameof(OnEvent), BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public DynamicWeakEventHandler(TEventSource eventSource, string eventName, EventHandler<TEventArgs> handler) : base(eventSource, handler, eventName)
        {
        }

        protected override void StartListening(TEventSource source)
        {
            if (this.eventInfo == null)
            {
                this.InitializeEventInfo(source);
            }

            this.eventInfo.AddEventHandler(source, this.eventHandler);
        }

        protected override void StopListening(TEventSource source)
        {
            this.eventInfo.RemoveEventHandler(source, this.eventHandler);
        }

        private void InitializeEventInfo(TEventSource eventSource)
        {
            this.eventInfo = eventSource.GetType().GetRuntimeEvent(this.eventName);

            if (this.eventInfo == null)
            {
                throw new ArgumentException($"The type ${eventSource.GetType().FullName} does not contain an event named {this.eventName}.");
            }

            this.eventHandler = OnEventMethodInfo.CreateDelegate(this.eventInfo.EventHandlerType, this);
        }
    }
}