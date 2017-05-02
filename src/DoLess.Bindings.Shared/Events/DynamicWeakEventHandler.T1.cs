using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DoLess.Bindings
{
    internal sealed class DynamicWeakEventHandler<TEventSource> : 
        WeakEventHandler<TEventSource, EventArgs>
        where TEventSource : class
    {
        private static readonly TypeInfo EventHandlerTypeInfo;
        private Delegate eventHandler;
        private EventInfo eventInfo;

        static DynamicWeakEventHandler()
        {
            EventHandlerTypeInfo = typeof(EventHandler).GetTypeInfo();
        }

        public DynamicWeakEventHandler(TEventSource eventSource, string eventName, EventHandler<EventArgs> handler) : base(eventSource, handler, eventName)
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
            this.eventInfo = null;
            this.eventHandler = null;
        }

        /// <summary>
        /// Creates a generic event handler used to handle <see cref="EventHandler{TEventArgs}"/>.
        /// </summary>
        /// <param name="eventInfo">The event info.</param>
        /// <param name="action">The action to call when the event is raised.</param>
        /// <returns></returns>
        /// <remarks>
        /// Inspired by the remarkable work of praeclarum: https://github.com/praeclarum/Bind/blob/master/src/Bind.cs.
        /// </remarks>
        private static Delegate CreateGenericEventHandler(EventInfo eventInfo, Action action)
        {
            var handlerType = eventInfo.EventHandlerType;
            var eventParameters = handlerType.GetMethod("Invoke").GetParameters();

            // lambda: (object x0, EventArgs x1) => action().
            var parameters = eventParameters.Select(p => Expression.Parameter(p.ParameterType, p.Name)).ToArray();
            var body = Expression.Call(Expression.Constant(action), action.GetType().GetMethod("Invoke"));
            var lambda = Expression.Lambda(body, parameters);

            return Delegate.CreateDelegate(handlerType, lambda.Compile(), "Invoke", false);
        }

        private void InitializeEventInfo(TEventSource eventSource)
        {
            this.eventInfo = TEventSourceType.GetRuntimeEvent(this.EventName);

            if (this.eventInfo == null)
            {
                throw new ArgumentException($"The type ${eventSource.GetType().FullName} does not contain an event named {this.EventName}.");
            }

            bool isClassicHandler = EventHandlerTypeInfo.IsAssignableFrom(eventInfo.EventHandlerType);
            this.eventHandler = isClassicHandler ?
                (EventHandler)this.OnEvent :
                CreateGenericEventHandler(eventInfo, this.OnEvent);
        }

        private void OnEvent()
        {
            this.OnEvent(null, EventArgs.Empty);
        }
    }
}