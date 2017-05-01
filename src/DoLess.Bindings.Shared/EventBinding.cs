using System;
using DoLess.Bindings.Helpers;

namespace DoLess.Bindings
{
    internal class EventBinding<TSource, TTarget, TEventArgs> :
        Binding<TSource, TTarget>,
        IEventBinding<TSource, TTarget, TEventArgs>
        where TSource : class
        where TTarget : class
        where TEventArgs : EventArgs
    {
        private WeakEventHandler<TTarget, TEventArgs> weakEventHandler;
        private readonly Func<TTarget, EventHandler<TEventArgs>, WeakEventHandler<TTarget, TEventArgs>> weakEventHandlerFactory;

        public EventBinding(IBinding<TSource, TTarget> binding, string eventName) :
            base((IHaveBindingSet<TSource, TTarget>)binding)
        {
            Check.NotNull(eventName, nameof(eventName));

            this.InitializeWeakEventHandler(eventName);
        }

        public EventBinding(IBinding<TSource, TTarget> binding, Func<TTarget, EventHandler<TEventArgs>, WeakEventHandler<TTarget, TEventArgs>> weakEventHandlerFactory) :
            base((IHaveBindingSet<TSource, TTarget>)binding)
        {
            Check.NotNull(weakEventHandlerFactory, nameof(weakEventHandlerFactory));

            this.weakEventHandlerFactory = weakEventHandlerFactory;
            this.weakEventHandler = this.weakEventHandlerFactory(this.BindingSet.Target, this.OnEventRaised);
        }

        public EventBinding(EventBinding<TSource, TTarget, TEventArgs> binding) :
            base((IHaveBindingSet<TSource, TTarget>)binding)
        {
            if (binding.weakEventHandlerFactory == null)
            {
                // It's necessary to recreate the binding because otherwise, the delegate is not on the right object.
                this.InitializeWeakEventHandler(binding.weakEventHandler.EventName);
            }
            else
            {
                this.weakEventHandler = binding.weakEventHandlerFactory(this.BindingSet.Target, this.OnEventRaised);
            }
        }

        public EventBinding(IEventBinding<TSource, TTarget, TEventArgs> binding) :
            this((EventBinding<TSource, TTarget, TEventArgs>)binding)
        {
        }

        protected string EventName => this.weakEventHandler?.EventName;

        protected virtual void OnEventRaised(object sender, TEventArgs args)
        {
        }

        protected virtual void InitializeWeakEventHandler(string eventName)
        {
            this.weakEventHandler = new DynamicWeakEventHandler<TTarget, TEventArgs>(this.BindingSet.Target, eventName, this.OnEventRaised);
        }
    }
}
