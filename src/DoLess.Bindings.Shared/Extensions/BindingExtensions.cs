using System;
using System.Linq.Expressions;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public static partial class BindingExtensions
    {
        public static IBinding<TSource, TTarget> Bind<TSource, TTarget>(this IBindableView<TSource> self, TTarget target)
            where TSource : class
            where TTarget : class
        {
            return Binding<TSource, TTarget>.CreateFromBindableView(self, target);
        }

        internal static IEventToCommandBinding<TSource, TTarget, TEventArgs, TCommand> EventTo<TSource, TTarget, TEventArgs, TCommand>(
            this IBinding<TSource, TTarget> self,
            Expression<Func<TSource, TCommand>> commandExpression,
            Func<TTarget, EventHandler<TEventArgs>, WeakEventHandler<TTarget, TEventArgs>> weakEventHandlerFactory,
            Expression<Func<TTarget, bool>> canExecutePropertyExpression = null)
            where TSource : class
            where TTarget : class
            where TEventArgs : EventArgs
            where TCommand : ICommand
        {
            IEventBinding<TSource, TTarget, TEventArgs> eventBinding = new EventBinding<TSource, TTarget, TEventArgs>(self, weakEventHandlerFactory);            
            return new EventToCommandBinding<TSource, TTarget, TEventArgs, TCommand>(eventBinding, commandExpression, canExecutePropertyExpression);
        }
    }
}
