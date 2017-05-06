using System;
using DoLess.Bindings.Helpers;
using System.Linq.Expressions;
using System.Windows.Input;

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
            base((IHaveLinkedBinding<TSource, TTarget>)binding)
        {
            Check.NotNull(eventName, nameof(eventName));

            this.InitializeWeakEventHandler(eventName);
        }

        public EventBinding(IBinding<TSource, TTarget> binding, Func<TTarget, EventHandler<TEventArgs>, WeakEventHandler<TTarget, TEventArgs>> weakEventHandlerFactory) :
            base((IHaveLinkedBinding<TSource, TTarget>)binding)
        {
            Check.NotNull(weakEventHandlerFactory, nameof(weakEventHandlerFactory));

            this.weakEventHandlerFactory = weakEventHandlerFactory;
            this.weakEventHandler = this.weakEventHandlerFactory(this.Target, this.OnEventRaised);
        }

        public EventBinding(EventBinding<TSource, TTarget, TEventArgs> binding) :
            base((IHaveLinkedBinding<TSource, TTarget>)binding)
        {
            if (binding.weakEventHandlerFactory == null)
            {
                // It's necessary to recreate the binding because otherwise, the delegate is not on the right object.
                this.InitializeWeakEventHandler(binding.weakEventHandler.EventName);
            }
            else
            {
                this.weakEventHandler = binding.weakEventHandlerFactory(this.Target, this.OnEventRaised);
            }

            binding.UnbindInternal();
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
            this.weakEventHandler = new DynamicWeakEventHandler<TTarget, TEventArgs>(this.Target, eventName, this.OnEventRaised);
        }

        public override void UnbindInternal()
        {
            base.UnbindInternal();
            this.weakEventHandler.Unsubscribe();
            this.weakEventHandler = null;
        }

        public IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand> To<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
            where TCommand : ICommand
        {
            return new EventToCommandBinding<TSource, TTarget, TEventArgs, TCommand>(this, commandExpression, null);
        }
    }
}
