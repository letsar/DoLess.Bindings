using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using DoLess.Bindings.Helpers;

namespace DoLess.Bindings
{
    internal class EventBinding<TSource, TTarget, TEventArgs> :
        Binding<TSource, TTarget>,
        IEventBinding<TSource, TTarget, TEventArgs>
        where TSource : class, INotifyPropertyChanged
        where TTarget : class
        where TEventArgs : EventArgs
    {
        protected readonly string eventName;
        private readonly DynamicWeakEventHandler<TTarget, TEventArgs> weakEventHandler;

        public EventBinding(IBinding<TSource, TTarget> binding, string eventName) :
            base((IHaveBindingSet<TSource, TTarget>)binding)
        {
            Check.NotNull(eventName, nameof(eventName));

            this.eventName = eventName;
            this.weakEventHandler = new DynamicWeakEventHandler<TTarget, TEventArgs>(this.BindingSet.Target, eventName, this.OnEventRaised);
        }

        public EventBinding(EventBinding<TSource, TTarget, TEventArgs> binding) :
            base((IHaveBindingSet<TSource, TTarget>)binding)
        {
            this.eventName = binding.eventName;

            // It's necessary to recreate the binding because otherwise, the delegate is not on the right object.
            this.weakEventHandler = new DynamicWeakEventHandler<TTarget, TEventArgs>(this.BindingSet.Target, eventName, this.OnEventRaised);
        }

        public EventBinding(IEventBinding<TSource, TTarget, TEventArgs> binding) :
            this((EventBinding<TSource, TTarget, TEventArgs>)binding)
        {
        }
        protected virtual void OnEventRaised(object sender, TEventArgs args)
        {
        }
    }
}
