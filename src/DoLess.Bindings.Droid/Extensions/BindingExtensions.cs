using System;
using System.Linq.Expressions;
using System.Windows.Input;

namespace DoLess.Bindings
{
    public static partial class BindingExtensions
    {
        internal static IEventToCommandBinding<TSource, TTarget, EventArgs<TItemProperty>, TCommand> EventTo<TSource, TTarget, TCommand, TItemProperty>(this IBinding<TSource, TTarget> self, Expression<Func<TSource, TCommand>> commandExpression, Func<TTarget, EventHandler<EventArgs<TItemProperty>>, WeakEventHandler<TTarget, EventArgs<TItemProperty>>> weakEventHandlerFactory, Expression<Func<TTarget, bool>> canExecutePropertyExpression = null)
            where TSource : class
            where TTarget : class, IBindableAdapter<TItemProperty>
            where TCommand : ICommand
            where TItemProperty : class
        {
            IEventBinding<TSource, TTarget, EventArgs<TItemProperty>> eventBinding = new EventBinding<TSource, TTarget, EventArgs<TItemProperty>>(self, weakEventHandlerFactory);
            return new EventToCommandBinding<TSource, TTarget, EventArgs<TItemProperty>, TCommand>(eventBinding, commandExpression, canExecutePropertyExpression);
        }
    }
}
