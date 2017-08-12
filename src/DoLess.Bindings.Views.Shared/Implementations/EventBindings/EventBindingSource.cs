using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;

namespace DoLess.Bindings
{
    internal class EventBindingSource<TSource, TTarget, TEventArgs> :
        Binding<TSource, TTarget>,
        IEventBindingSource<TSource, TTarget, TEventArgs>
        where TSource : class
        where TTarget : class
        where TEventArgs : EventArgs
    {
        private readonly IBinding<TSource, TTarget> parent;
        private readonly Action<TTarget, EventHandler<TEventArgs>> addHandler;
        private readonly Action<TTarget, EventHandler<TEventArgs>> removeHandler;

        public EventBindingSource(IBinding<TSource, TTarget> binding, Action<TTarget, EventHandler<TEventArgs>> addHandler, Action<TTarget, EventHandler<TEventArgs>> removeHandler) :
            base(binding)
        {
            this.parent = binding;
            this.addHandler = addHandler;
            this.removeHandler = removeHandler;
        }

        public IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand> To<TCommand>(Expression<Func<TSource, TCommand>> commandExpression)
                    where TCommand : ICommand
        {
            return this.To<TCommand>(commandExpression, null);
        }

        public IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand> To<TCommand>(Expression<Func<TSource, TCommand>> commandExpression, Expression<Func<TTarget, bool>> targetCanExecutePropertyExpression)
                    where TCommand : ICommand
        {
            return new EventToCommandBinding<TSource, TTarget, TEventArgs, TCommand>(this.parent, this.addHandler, this.removeHandler, commandExpression, targetCanExecutePropertyExpression);
        }
    }
}
