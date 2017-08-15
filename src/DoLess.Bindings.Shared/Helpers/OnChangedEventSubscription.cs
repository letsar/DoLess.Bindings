using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DoLess.Bindings
{
    /// <summary>
    /// Class used to add and remove event handlers to a type.
    /// </summary>
    internal class OnChangedEventSubscription<T> : IDisposable
        where T : class
    {
        private static readonly Dictionary<string, EventInfo> EventInfoCache = new Dictionary<string, EventInfo>();
        private static readonly Type ThisType = typeof(T);
        private static readonly object EventInfoCacheLock = new object();

        private readonly string propertyName;
        private T target;
        private Action onChanged;
        private Delegate eventHandler;

        public OnChangedEventSubscription(string propertyName, T target, Action onChanged)
        {
            this.propertyName = propertyName;
            this.target = target;
            this.onChanged = onChanged;

            this.AddHandler();
        }

        private void AddHandler()
        {
            EventInfo eventInfo = this.GetCachedEventInfo();
            if (eventInfo != null && this.target != null)
            {
                this.eventHandler = CreateGenericEventHandler(eventInfo, this.onChanged);
                eventInfo.AddEventHandler(this.target, this.eventHandler);
            }
        }

        private void RemoveHandler()
        {
            EventInfo eventInfo = this.GetCachedEventInfo();
            if (eventInfo != null && this.eventHandler != null)
            {
                eventInfo.RemoveEventHandler(this.target, this.eventHandler);
                this.eventHandler = null;
            }
        }

        private EventInfo GetCachedEventInfo() => GetCachedEventInfo(this.propertyName);

        public void Dispose()
        {
            this.RemoveHandler();
            this.onChanged = null;
            this.target = null;            
        }

        private static Delegate CreateGenericEventHandler(EventInfo eventInfo, Action action)
        {
            var handlerType = eventInfo.EventHandlerType;
            var eventParams = handlerType.GetMethod("Invoke")
                                         .GetParameters();

            //lambda: (object x0, EventArgs x1) => d()
            var parameters = eventParams.Select(p => Expression.Parameter(p.ParameterType, p.Name))
                                        .ToArray();
            var body = Expression.Call(Expression.Constant(action), action.GetType().GetMethod("Invoke"));
            var lambda = Expression.Lambda(body, parameters);

            return Delegate.CreateDelegate(handlerType, lambda.Compile(), "Invoke", false);
        }

        private static EventInfo GetCachedEventInfo(string propertyName)
        {
            lock (EventInfoCacheLock)
            {
                if (!EventInfoCache.TryGetValue(propertyName, out EventInfo eventInfo))
                {
                    eventInfo = GetFirstEventInfo(propertyName);
                    EventInfoCache[propertyName] = eventInfo;
                }

                return eventInfo;
            }
        }

        private static EventInfo GetFirstEventInfo(string propertyName)
        {
            var eventNames = new string[]
            {
                propertyName + "Changed",
                "EditingChanged",
                "ValueChanged",
                "Changed"
            };
            return GetFirstEventInfo(eventNames);
        }

        private static EventInfo GetFirstEventInfo(string[] eventNames)
        {
            return eventNames.Select(x => ThisType.GetRuntimeEvent(x))
                             .FirstOrDefault(x => x != null);
        }
    }
}
